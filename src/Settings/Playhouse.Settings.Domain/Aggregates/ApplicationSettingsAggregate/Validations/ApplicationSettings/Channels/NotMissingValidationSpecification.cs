using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.Channels
{
    public sealed class NotMissingValidationSpecification : IValidationSpecification<BrowserChannel>
    {
        private readonly IEnumerable<BrowserChannel> _channels;

        public NotMissingValidationSpecification(IEnumerable<BrowserChannel> channels) 
        {
            ArgumentNullException.ThrowIfNull(channels);

            _channels = channels;
        }

        public Result IsSatisfiedBy(BrowserChannel channel)
        {
            return _channels.Contains(channel) == false
                ? Result.Fail([new ApplicationSettingsChannelNotFoundError(channel)])
                : Result.Ok();
        }
    }
}
