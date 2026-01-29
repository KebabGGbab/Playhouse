using Microsoft.Playwright;
using Playhouse.Core.Models;

namespace Playhouse.Core.Services.PlaywrightService.Abstractions
{
	public interface IPlaywrightFactory
	{
		Task<IBrowserContext> CreateBrowserAsync(BrowserProfile profile, BotInfo bot);
	}
}
