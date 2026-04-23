namespace Playhouse.Domain.SharedKernel.Results
{
    public abstract class Error
    {
        public abstract string Code { get; }

        public string Message { get; }

        protected Error(string message)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message);

            Message = message;
        }

        public static implicit operator string(Error error)
        {
            return error.Code;
        }
    }
}
