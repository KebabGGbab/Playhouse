namespace Playhouse.SharedKernel.Domain.Results
{
    public abstract class Error
    {
        public string Code { get; }

        public string Message { get; }

        protected Error(string message, string code)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message);
            ArgumentException.ThrowIfNullOrWhiteSpace(code);

            Message = message;
            Code = code;
        }
    }
}
