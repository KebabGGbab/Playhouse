using Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Errors.ApplicationSettings;
using Playhouse.SharedKernel.Domain.Results;
using Playhouse.SharedKernel.Domain.Specifications.Validation;

namespace Playhouse.Settings.Domain.Aggregates.ApplicationSettingsAggregate.Validations.ApplicationSettings;

public sealed class ApplicationSettingsChannelNotNullValidationSpecification : IValidationSpecification<BrowserChannel>
{
    public Result IsSatisfiedBy(BrowserChannel channel)
    {
        return channel == null
            ? Result.Fail([new ApplicationSettingsChannelNullError()])
            : Result.Ok();
    }
}

public sealed class ApplicationSettingsChannelNotAlreadyAddedValidaitonSpecification : IValidationSpecification<BrowserChannel>
{
    private readonly IEnumerable<BrowserChannel> _channels;

    public ApplicationSettingsChannelNotAlreadyAddedValidaitonSpecification(IEnumerable<BrowserChannel> channels)
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

public sealed class ApplicationSettingsChannelNotMissingValidationSpecification : IValidationSpecification<BrowserChannel>
{
    private readonly IEnumerable<BrowserChannel> _channels;

    public ApplicationSettingsChannelNotMissingValidationSpecification(IEnumerable<BrowserChannel> channels)
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

public sealed class ApplicationSettingsChannelCanAddValidationSpecification : IValidationSpecification<BrowserChannel>
{
    private readonly IValidationSpecification<BrowserChannel> _validations;

    public ApplicationSettingsChannelCanAddValidationSpecification(IEnumerable<BrowserChannel> channels)
    {
        _validations = new ApplicationSettingsChannelNotNullValidationSpecification()
            .Then(new ApplicationSettingsChannelNotAlreadyAddedValidaitonSpecification(channels));
    }

    public Result IsSatisfiedBy(BrowserChannel channel)
    {
        return _validations.IsSatisfiedBy(channel);
    }
}

public sealed class ApplicationSettingsChannelCanRemoveValidationSpecification
{
    private readonly IValidationSpecification<BrowserChannel> _validations;

    public ApplicationSettingsChannelCanRemoveValidationSpecification(IEnumerable<BrowserChannel> channels)
    {
        _validations = new ApplicationSettingsChannelNotNullValidationSpecification()
            .Then(new ApplicationSettingsChannelNotMissingValidationSpecification(channels));
    }

    public Result IsSatisfiedBy(BrowserChannel channel)
    {
        return _validations.IsSatisfiedBy(channel);
    }
}