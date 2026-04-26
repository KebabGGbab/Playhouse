using Playhouse.SharedKernel.Domain.Exceptions;

namespace Playhouse.SharedKernel.Domain.Results
{
    public sealed class Result<T> : Result
    {
        private readonly T? _value;

        public T Value
        {
            get
            {
                TryGetValueFromFailureResultDomainException.ThrowIfFailure(IsFailure);

                return _value!;
            }
        }

        internal Result(T? value, bool success, IEnumerable<Error>? errors)
            : base(success, errors) 
        {
            _value = value;
        }
    }
}
