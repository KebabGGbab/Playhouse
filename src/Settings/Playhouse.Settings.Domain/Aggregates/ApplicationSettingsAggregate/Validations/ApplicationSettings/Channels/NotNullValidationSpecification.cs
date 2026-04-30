using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings.Channels
{
    public sealed class NotNullValidationSpecification : IValidationSpecification<BrowserChannel>
    {
        public Result IsSatisfiedBy(BrowserChannel channel)
        {
            return channel == null
                ? Result.Fail([new ApplicationSettingsChannelNullError()])
                : Result.Ok();
        }
    }
}
