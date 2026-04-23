using Playhouse.Domain.SharedKernel.Resources.Strings;

namespace Playhouse.Domain.SharedKernel.Results
{
    public sealed class Result<T> : Result
    {
        private readonly T? _value;

        public T Value => IsSuccess ? _value! : throw new InvalidOperationException(ExceptionMessages.ResultGetValueFromFailure);

        internal Result(T? value, bool success, IReadOnlyCollection<Error> errors)
            : base(success, errors) 
        {
            _value = value;
        }
    }
}
