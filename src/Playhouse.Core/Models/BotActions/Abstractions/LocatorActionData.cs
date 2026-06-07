using Microsoft.Playwright;

namespace Playhouse.Core.Models.BotActions.Abstractions
{
    public class LocatorActionData
    {
        public string Action { get; }

        public AriaRole? Role { get; }
         
        public string? Id { get; }

        public string? Text { get; }

        public string Selector { get; }

        public LocatorActionData(string action, string selector, AriaRole? role = null, string? id = null, string? text = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(action);
            ArgumentException.ThrowIfNullOrWhiteSpace(selector);

            Action = action;
            Selector = selector;
            Role = role;
            Id = id;
            Text = text;
        }

        public ILocator GetLocator(IPage page)
        {
            ArgumentNullException.ThrowIfNull(page);

            if (Role != null)
            {
                return page.GetByRole((AriaRole)Role, new PageGetByRoleOptions()
                {
                    Name = Text,
                    Exact = Text == null
                            ? null
                            : Text.Length < 100
                });
            }
            else if (Id != null)
            {
                return page.Locator($"#{Id}");
            }
            else
            {
                return page.Locator(Selector);
            }
        }
    }
}
