using Playhouse.Domain.SharedKernel.Resources.Strings;

namespace Playhouse.Domain.SharedKernel.Results
{
    public class Result
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public IReadOnlyCollection<Error> Errors { get; }

        protected Result(bool success, IReadOnlyCollection<Error> errors)
        {
            ArgumentNullException.ThrowIfNull(errors);

            if (!success && errors.Count == 0)
            {
                throw new ArgumentException(ExceptionMessages.ResultIsFailureWithoutErrors);
            }

            IsSuccess = success;
            Errors = errors ?? [];
        }

        public static Result Ok() => new(true, []);

        public static Result<T> Ok<T>(T value) => new(value, true, []);

        public static Result Fail(IEnumerable<Error> errors) => new(false, errors.ToList().AsReadOnly());

        public static Result<T> Fail<T>(IEnumerable<Error> errors) => new(default, false, errors.ToList().AsReadOnly());
    }
}
