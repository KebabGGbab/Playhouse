using Microsoft.Playwright;
using Playhouse.Core.Enums;
using Playhouse.Core.Models;
using Playhouse.Core.Models.BrowserEvents;
using Playhouse.Core.Models.PlaywrightDecorator;

namespace Playhouse.Core.Test.Tools
{
    internal static class GeneratorTestDbData
    {
        public static BotInfo[] GenerateBotsInfo()
        {
            BotInfo[] botsInfo =
                [
                    new() { Name = "test", Browser = Enums.BrowserType.WebKit },
                    new() { Name = "2", Browser = Enums.BrowserType.Chromium },
                    new() { Name = "Play", Browser = Enums.BrowserType.Firefox },
                    new() { Name = "we", Browser = Enums.BrowserType.Chromium },
                    new() { Name="Bot5", Browser = Enums.BrowserType.Firefox },
                    new() { Name="6", Browser = Enums.BrowserType.Chromium },
                ];

            botsInfo[0].BrowserEvents.Add(new PageCreatedBrowserEvent()
            {
                BotInfo = botsInfo[0],
                Number = 1
            });

            botsInfo[1].BrowserEvents.Add(new LocatorClickBrowserEvent(
                new LocatorClickOptionsStrictDecorator() {
                    Position = new Position()
                    {
                        X = 10,
                        Y = 3
                    }
                })
            {
                BotInfo = botsInfo[1],
                Number = 1
            });

            botsInfo[2].BrowserEvents.Add(new PageClosedBrowserEvent(
                new PageCloseOptionsStrictDecorator()
                {
                    RunBeforeUnload = true,
                    Reason = "Так надо"
                })
            {
                BotInfo = botsInfo[2],
                Number = 1
            });
            botsInfo[2].BrowserEvents.Add(new PageClosedBrowserEvent(
                new PageCloseOptionsStrictDecorator()
                {
                    RunBeforeUnload = false
                })
            {
                BotInfo = botsInfo[2],
                Number = 2
            });
            botsInfo[2].BrowserEvents.Add(new PageClosedBrowserEvent()
            {
                BotInfo = botsInfo[2],
                Number = 3
            });

            botsInfo[3].BrowserEvents.Add(new BrowserContextClosedBrowserEvent()
            {
                BotInfo = botsInfo[3],
                Number = 1
            });
            botsInfo[3].BrowserEvents.Add(new BrowserContextClosedBrowserEvent(
                new BrowserContextCloseOptionsStrictDecorator()
                {
                    Reason = "Причина"
                })
            {
                BotInfo = botsInfo[3],
                Number = 2
            });

            botsInfo[4].BrowserEvents.Add(new PageGoToBrowserEvent(
                "https://playhoouse.ru/",
                new PageGoToOptionsStrictDecorator())
            {
                BotInfo = botsInfo[4],
                Number = 1 }
            );
            botsInfo[4].BrowserEvents.Add(new PageGoToBrowserEvent(
                "https://playhoouse2.ru/",
                new PageGoToOptionsStrictDecorator()
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                    Timeout = 10000,
                    Referer = "Строка"
                })
            {
                BotInfo = botsInfo[4],
                Number = 2
            });

            botsInfo[5].BrowserEvents.Add(new LocatorClickBrowserEvent(
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
                BotInfo = botsInfo[5],
                Number = 1
            });
            botsInfo[5].BrowserEvents.Add(new LocatorClickBrowserEvent()
            {
                BotInfo = botsInfo[5],
                Number = 2
            });

            return botsInfo;
        }

        public static BrowserProfile[] GenerateBrowserProfiles()
        {
            BrowserProfile[] profiles =
                [
                    new BrowserProfile()
                    {
                        Name = "test",
                        Options = new()
                        {
                            AcceptDownloads = false,
                            Channel = BrowserChannels.ChromeBeta.ToString(),
                            SlowMo = 1,
                        }
                    },
                    new BrowserProfile()
                    {
                        Name = "Profile1",
                        Options = new()
                    },
                    new BrowserProfile()
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
