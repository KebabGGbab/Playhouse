using Playhouse.Core.Models.BotActions.Abstractions;
using Playhouse.Core.Models.PlaywrightDecorator;

namespace Playhouse.Core.Models.BotActions
{
    public class PageGoToBotAction : PageBotAction
    {
        public string Url 
        { 
            get;
            set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);

                field = value;
            }
        } = null!;

        public PageGoToOptionsStrictDecorator Options { get; } = null!;

        // Конструктор для EntityFramework
        private PageGoToBotAction()
        {
        }

        public PageGoToBotAction(string url, PageGoToOptionsStrictDecorator? options = null)
        {
            Url = url;
            Options = options ?? new PageGoToOptionsStrictDecorator();
        }

        public override void Accept(IBotActionVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor);

            visitor.Visit(this);
        }
    }
}
