using Microsoft.Playwright;
using Playhouse.Core.Enums;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BotActions;
using Playhouse.Core.Models.PlaywrightDecorator;

namespace Playhouse.Core.Test.Tools
{
    internal static class GeneratorTestDbData
    {
        public static BotConfiguration[] GenerateBots()
        {
            BotConfiguration[] botsInfo =
                [
                    new() { Name = "test", Browser = Enums.BrowserType.WebKit },
                    new() { Name = "2", Browser = Enums.BrowserType.Chromium },
                    new() { Name = "Play", Browser = Enums.BrowserType.Firefox },
                    new() { Name = "we", Browser = Enums.BrowserType.Chromium },
                    new() { Name="Bot5", Browser = Enums.BrowserType.Firefox },
                    new() { Name="6", Browser = Enums.BrowserType.Chromium },
                ];

            botsInfo[0].Actions.Add(new PageCreatedBotAction()
            {
                Bot = botsInfo[0],
                Number = 1
            });

            LocatorClickBotAction locatorClick = new(new LocatorClickOptionsStrictDecorator())
            { 
                Bot = botsInfo[1], 
                Number = 1
            };
            locatorClick.Options.Position.X = 10;
            locatorClick.Options.Position.Y = 3;
            botsInfo[1].Actions.Add(locatorClick);

            botsInfo[2].Actions.Add(new PageClosedBotAction(
                new PageCloseOptionsStrictDecorator()
                {
                    RunBeforeUnload = true,
                    Reason = "Так надо"
                })
            {
                Bot = botsInfo[2],
                Number = 1
            });
            botsInfo[2].Actions.Add(new PageClosedBotAction(
                new PageCloseOptionsStrictDecorator()
                {
                    RunBeforeUnload = false
                })
            {
                Bot = botsInfo[2],
                Number = 2
            });
            botsInfo[2].Actions.Add(new PageClosedBotAction()
            {
                Bot = botsInfo[2],
                Number = 3
            });

            botsInfo[3].Actions.Add(new BrowserContextClosedBotAction()
            {
                Bot = botsInfo[3],
                Number = 1
            });
            botsInfo[3].Actions.Add(new BrowserContextClosedBotAction(
                new BrowserContextCloseOptionsStrictDecorator()
                {
                    Reason = "Причина"
                })
            {
                Bot = botsInfo[3],
                Number = 2
            });

            botsInfo[4].Actions.Add(new PageGoToBotAction(
                "https://playhoouse.ru/",
                new PageGoToOptionsStrictDecorator())
            {
                Bot = botsInfo[4],
                Number = 1 }
            );
            botsInfo[4].Actions.Add(new PageGoToBotAction(
                "https://playhoouse2.ru/",
                new PageGoToOptionsStrictDecorator()
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                    Timeout = 10000,
                    Referer = "Строка"
                })
            {
                Bot = botsInfo[4],
                Number = 2
            });

            botsInfo[5].Actions.Add(new LocatorClickBotAction(
                new LocatorClickOptionsStrictDecorator()
                {
                    Button = MouseButton.Middle,
                    ClickCount = 3,
                    Delay = 530,
                    Force = true,
                    Steps = 7,
                    Timeout = 15000,
                    Trial = true
                })
            {
                Bot = botsInfo[5],
                Number = 1
            });
            botsInfo[5].Actions.Add(new LocatorClickBotAction()
            {
                Bot = botsInfo[5],
                Number = 2
            });

            return botsInfo;
        }

        public static BrowserConfiguration[] GenerateProfiles()
        {
            BrowserConfiguration[] profiles =
                [
                    new BrowserConfiguration()
                    {
                        Name = "test",
                        Options = new()
                        {
                            AcceptDownloads = false,
                            Channel = BrowserChannels.ChromeBeta.ToString(),
                            SlowMo = 1,
                        }
                    },
                    new BrowserConfiguration()
                    {
                        Name = "Profile1",
                        Options = new()
                    },
                    new BrowserConfiguration()
                    {
                        Name = "Profile2",
                        Options = new()
                        {
                            AcceptDownloads = false,
                            Channel = BrowserChannels.ChromeBeta.ToString(),
                            ChromiumSandbox = true,
                            DownloadsPath = "C://Downloads",
                            Headless = false,
                            SlowMo = 1,
                        }
                    }
                ];

            return profiles;
        }
    }
}
