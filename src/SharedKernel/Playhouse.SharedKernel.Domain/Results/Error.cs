namespace Playhouse.SharedKernel.Domain.Results
{
    public abstract class Error
    {
        private static readonly IReadOnlyDictionary<string, object> _emptyDetails = new Dictionary<string, object>().AsReadOnly();

        public string Code { get; }

        public string Message { get; }

        public IReadOnlyDictionary<string, object> Details { get; }

        protected Error(string message, string code)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message);
            ArgumentException.ThrowIfNullOrWhiteSpace(code);

            Message = message;
            Code = code;
            Details = _emptyDetails;
        }

        protected Error(string message, string code, IReadOnlyDictionary<string, object> details)
            : this(message, code)
        {
            ArgumentNullException.ThrowIfNull(details);

            Details = details;
        }
    }
}
