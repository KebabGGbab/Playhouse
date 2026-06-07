using Playhouse.Core.Models.BotActions;

namespace Playhouse.ViewModels.ViewModels.BotActionViewModels
{
    public class BrowserContextCreatedBotActionViewModel : BotActionViewModel<BrowserContextCreatedBotAction>
    {
        public BrowserContextCreatedBotActionViewModel(BrowserContextCreatedBotAction action)
            : base(action)
        {
        }

        protected override bool CheckModified()
        {
            return false;
        }

        protected override Task SaveChangesCoreAsync()
        {
            return Task.CompletedTask;
        }

        protected override void CancelChangesCore()
        {
        }
    }
}
