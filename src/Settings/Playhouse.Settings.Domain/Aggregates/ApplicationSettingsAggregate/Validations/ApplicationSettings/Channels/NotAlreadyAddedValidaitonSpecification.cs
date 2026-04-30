using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.Channels
{
    public sealed class NotAlreadyAddedValidaitonSpecification : IValidationSpecification<BrowserChannel>
    {
        private readonly IEnumerable<BrowserChannel> _channels;

        public NotAlreadyAddedValidaitonSpecification(IEnumerable<BrowserChannel> channels)
        {
            ArgumentNullException.ThrowIfNull(channels);

            _channels = channels;
        }

        public Result IsSatisfiedBy(BrowserChannel channel)
        {
            return _channels.Contains(channel)
                ? Result.Fail([new ApplicationSettingsChannelExistError(channel)])
                : Result.Ok();
        }
    }
}
