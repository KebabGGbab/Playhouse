using Playhouse.SharedKernel.Domain.Results;

namespace Playhouse.Settings.Application.Validation.Errors
{
    public sealed class ValidationError : Error
    {
        private const string CODE = "ValidationError";

        public ValidationError(string message) : base(message, CODE)
        {
        }

        public ValidationError(string message, IReadOnlyDictionary<string, object> details) : base(message, CODE, details)
        {
        }
    }
}
