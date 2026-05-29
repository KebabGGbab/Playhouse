using Playhouse.SharedKernel.Domain.Exceptions;

namespace Playhouse.SharedKernel.Domain.Results
{
    public class Result
    {
        private readonly static IReadOnlyCollection<Error> _errorsEmpty = [];

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public IReadOnlyCollection<Error> Errors { get; }

        protected Result()
        {
            IsSuccess = true;
            Errors = _errorsEmpty;
        }

        protected Result(IEnumerable<Error> errors)
        {
            FailureResultNotContainErrorDomainException.ThrowIfErrorCollectionEmpty(errors);

            IsSuccess = false;
            Errors = errors.ToList().AsReadOnly();
        }

        public static Result Ok() => new();

        public static Result<T> Ok<T>(T value) => new(value);

        public static Result Fail(IEnumerable<Error> errors) => new(errors);

        public static Result<T> Fail<T>(IEnumerable<Error> errors) => new(errors);
    }
}
