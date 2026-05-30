namespace Playhouse.Core.Services.BotRunningService.Abstrtactions
{
    public interface IBotJobManagerFactory
    {
        BotJobManager Create(BotJobContext context);
    }
}
