using System.Reflection;
using KebabGGbab.Primitives.Extensions;
using Playhouse.Domain.SharedKernel.SeedWork;
using Playhouse.Settings.Domain.Resources.Strings;

namespace Playhouse.Settings.Domain.AggregatesModel.ApplicationSettingsAggregate
{
    public sealed class DirectoryPath : ValueObject
    {
        public static DirectoryPath Default { get; } = new DirectoryPath(GetDefaultPath());

        public string Path { get; }

        private DirectoryPath(string path)
        {
            Path = path;
        }

        public static Result<DirectoryPath> Create(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                // Если путь не указан, то нет смысла продолжать собирать следующие ошибки, из-за чего сразу возвращаем ошибку.
                return Result<DirectoryPath>.Fail(ErrorMessages.DirectoryPathNotSpecified);
            }

            List<string> errors = [];
            path = path.Trim();

            if (System.IO.Path.CheckHasInvalidChars(path))
            {
                errors.Add(ErrorMessages.DirectoryPathContainsInvalidChars);
            }

            if (!System.IO.Path.IsPathFullyQualified(path))
            {
                errors.Add(ErrorMessages.DirectoryPathMustAbsolute);
            }

            return errors.Count > 0 ? Result<DirectoryPath>.Fail(errors.ToArray()) : Result<DirectoryPath>.Ok(new DirectoryPath(path));
        }

        public static string GetDefaultPath()
        {
            return System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Assembly.GetExecutingAssembly().GetName().Name!);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Path.ToUpperInvariant();
        }
    }
}
