using Microsoft.Playwright;

namespace Playhouse.ViewModels.EventArguments
{
    public class PositionChangedEventArgs : EventArgs
    {
        public Position? NewPosition { get; }

        public PositionChangedEventArgs(Position? position) 
        {
            NewPosition = position;
        }
    }
}
