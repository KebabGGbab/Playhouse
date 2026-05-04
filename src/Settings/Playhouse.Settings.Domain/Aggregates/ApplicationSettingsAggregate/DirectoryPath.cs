using Playhouse.SharedKernel.Domain.BaseModels;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Exceptions.DirectoryPath;
using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.DirectoryPath;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate
{
    public sealed class DirectoryPath : ValueObject
    {
        private static readonly DirectoryPathPathNotEmptyValidationSpecification _pathNotEmptyValidation;
        private static readonly DirectoryPathPathValidationSpecification _pathValidation;

        public static DirectoryPath Default { get; }

        public string Path { get; }

        static DirectoryPath()
        {
            _pathNotEmptyValidation = new DirectoryPathPathNotEmptyValidationSpecification();
            _pathValidation = new DirectoryPathPathValidationSpecification();

            string defaultPath = GetDefaultPath();
            Result<DirectoryPath> defaultDirectoryPath = Create(defaultPath);
            
            if (defaultDirectoryPath.IsFailure)
            {
                throw new InvalidDefaultPathDomainException(defaultPath, defaultDirectoryPath.Errors.Select(e => e.Code));
            }

            Default = defaultDirectoryPath.Value;
        }

        private DirectoryPath(string path)
        {
            Path = path;
        }

        public static Result CanCreate(string path)
        {
            Result resultEmptyValidation = _pathNotEmptyValidation.IsSatisfiedBy(path);

            if (resultEmptyValidation.IsFailure)
            {
                return resultEmptyValidation;
            }

            path = TransformPath(path);

            return _pathValidation.IsSatisfiedBy(path);
        }

        public static Result<DirectoryPath> Create(string path)
        {
            Result canCreate = CanCreate(path);

            if (canCreate.IsFailure)
            {
                return Result.Fail<DirectoryPath>(canCreate.Errors);
            }

            path = TransformPath(path);

            return Result.Ok(new DirectoryPath(path));
        }

        private static string GetDefaultPath()
        {
            return System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                nameof(Playhouse));
        }

        private static string TransformPath(string path)
        {
            return path.Trim();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Path.ToUpperInvariant();
        }
    }
}
