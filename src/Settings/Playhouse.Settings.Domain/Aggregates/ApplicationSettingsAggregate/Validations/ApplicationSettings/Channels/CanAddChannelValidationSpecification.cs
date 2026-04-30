using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.Channels
{
    public sealed class CanAddChannelValidationSpecification : IValidationSpecification<BrowserChannel>
    {
        private readonly IValidationSpecification<BrowserChannel> _validations;

        public CanAddChannelValidationSpecification(IEnumerable<BrowserChannel> channels)
        {
            _validations = new NotNullValidationSpecification()
                .Then(new NotAlreadyAddedValidaitonSpecification(channels));
        }

        public Result IsSatisfiedBy(BrowserChannel channel)
        {
            return _validations.IsSatisfiedBy(channel);
        }
    }
}
