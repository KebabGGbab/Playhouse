using System.Text;
using Playhouse.SharedKernel.Domain.Exceptions;
using Playhouse.Settings.Domain.Resources.Strings;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Exceptions.DirectoryPath
{
    public sealed class InvalidDefaultPathDomainException : DomainException
    {
        private static readonly CompositeFormat _messageFormat = CompositeFormat.Parse(DomainExceptionMessages.DirectoryPathInvalidDefault); 

        public string? Path { get; }

        public IEnumerable<string> Reasons { get; }

        public InvalidDefaultPathDomainException(string? path, IEnumerable<string> reasons)
            : base(string.Format(null, _messageFormat, path))
        {
            ArgumentNullException.ThrowIfNull(reasons);

            Path = path;
            Reasons = reasons;
        }
    }
}
