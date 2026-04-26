using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Playhouse.SharedKernel.Domain.Exceptions;

namespace Playhouse.SharedKernel.Domain.Results
{
    public class Result
    {
        private readonly ReadOnlyCollection<Error>? _errors;

        [MemberNotNullWhen(false, nameof(Errors))]
        public bool IsSuccess { get; }

        [MemberNotNullWhen(true, nameof(Errors))]
        public bool IsFailure => !IsSuccess;

        public IReadOnlyCollection<Error>? Errors => _errors;

        protected Result(bool success, IEnumerable<Error>? errors)
        {
            if (success == false)
            {
                FailureResultNotContainErrorDomainException.ThrowIfErrorCollectionEmpty(errors);

                _errors = errors.ToList().AsReadOnly();
            }

            IsSuccess = success;
        }

        public static Result Ok() => new(true, null);

        public static Result<T> Ok<T>(T value) => new(value, true, null);

        public static Result Fail(IEnumerable<Error> errors) => new(false, errors);

        public static Result<T> Fail<T>(IEnumerable<Error> errors) => new(default, false, errors);
    }
}
