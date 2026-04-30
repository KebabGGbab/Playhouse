using Playhouse.SharedKernel.Domain.Events;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.DomainEvents
{
    public sealed class PathToDataChangedDomainEvent : IDomainEvent
    {
        public DirectoryPath OldPathToData { get; }

        public DirectoryPath NewPathToData { get; }

        public PathToDataChangedDomainEvent(DirectoryPath oldPathToData, DirectoryPath newPathToData)
        {
            OldPathToData = oldPathToData;
            NewPathToData = newPathToData;
        }
    }
}
