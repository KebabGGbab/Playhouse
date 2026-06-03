using Playhouse.Core.Models.BotActions;

namespace Playhouse.ViewModels.ViewModels.BotActionViewModels
{
    public class PageCreatedBotActionViewModel : BotActionViewModel<PageCreatedBotAction>
    {
        public PageCreatedBotActionViewModel(PageCreatedBotAction action) 
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
