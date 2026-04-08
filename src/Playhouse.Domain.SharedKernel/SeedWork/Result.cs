namespace Playhouse.Domain.SharedKernel.SeedWork
{
    public sealed class Result<T>
    {
        private readonly List<string> _errors;

        public T? Value { get; }

        public IReadOnlyList<string> Errors => _errors.AsReadOnly();

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        private Result(T? value, bool success, string[]? errors)
        {
            Value = value;
            IsSuccess = success;
            _errors = errors == null ? [] : errors.ToList();
        }

        public static Result<T> Ok(T value) => new(value, true, null);

        public static Result<T> Fail(params string[] errors) => new(default, false, errors);
    }
}
