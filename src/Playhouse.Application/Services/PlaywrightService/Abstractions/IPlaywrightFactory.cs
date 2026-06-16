using Microsoft.Playwright;
using Playhouse.Domain;

namespace Playhouse.Application.Services.PlaywrightService.Abstractions
{
	public interface IPlaywrightFactory
	{
		Task<IBrowserContext> CreateBrowserAsync(BrowserConfiguration profile, BotConfiguration bot);
	}
}
