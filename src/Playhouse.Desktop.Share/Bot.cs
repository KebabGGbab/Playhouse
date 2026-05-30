using Microsoft.Playwright;

namespace PlayhouseShare
{
    public abstract class Bot
    {
        public abstract Task RunAsync(IBrowserContext browser, CancellationToken? cancellation = null);
    }
}
