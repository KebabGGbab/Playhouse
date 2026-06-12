using Microsoft.Playwright;

namespace Playhouse.Core.Models.BotActions.Abstractions
{
    public class LocatorActionData
    {
        public ActionTypes Action { get; set; } = null!;

        public AriaRole Role { get; set; }
         
        public string? Id { get; set; }

        public string? Text { get; set; }

        public string Selector { get; set; } = null!;

        // Конструктор для EntityFramework
        private LocatorActionData()
        {
        }

        public LocatorActionData(ActionTypes action, string selector, AriaRole role, string? id = null, string? text = null)
        {
            ArgumentNullException.ThrowIfNull(action);
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

            if (Role != AriaRole.None && string.IsNullOrWhiteSpace(Text) == false)
            {
                return page.GetByRole(Role, new PageGetByRoleOptions()
                {
                    Name = Text,
                    Exact = Text.Length < 100
                });
            }
            else if (string.IsNullOrWhiteSpace(Id) == false)
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
