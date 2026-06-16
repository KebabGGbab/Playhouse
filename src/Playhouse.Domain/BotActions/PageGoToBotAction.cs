using Playhouse.Domain.BotActions.Abstractions;
using Playhouse.Domain.PlaywrightDecorator;

namespace Playhouse.Domain.BotActions
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

        public PageGoToBotAction(BotConfiguration configuration, string url, PageGoToOptionsStrictDecorator? options = null)
            : base(configuration)
        {
            Url = url;
            Options = options ?? new PageGoToOptionsStrictDecorator();
        }

        public override T Accept<T>(IBotActionVisitor<T> visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor);

            return visitor.Visit(this);
        }

        public override async Task Accept(IBotActionAsyncVisitor visitor)
        {
            ArgumentNullException.ThrowIfNull(visitor);

            await visitor.VisitAsync(this)
                .ConfigureAwait(false);
        }
    }
}
