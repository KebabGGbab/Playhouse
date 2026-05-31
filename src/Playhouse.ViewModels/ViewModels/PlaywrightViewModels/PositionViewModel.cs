using KebabGGbab.CommunityToolkit.MVVM.Extensions.ViewModelAbstractions;
using Microsoft.Playwright;

namespace Playhouse.ViewModels.ViewModels.PlaywrightViewModels
{
    public class PositionViewModel : EditableViewModel
    {
        private readonly Position _position;

        private float _x;
        private float _y;

        public float X
        {
            get => _position.X;
            set
            {
                if (SetProperty(ref _x, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public float Y
        {
            get => _position.Y;
            set
            {
                if (SetProperty(ref _y, value))
                {
                    IsModified = CheckModified();
                }
            }
        }

        public PositionViewModel(Position position)
        {
            ArgumentNullException.ThrowIfNull(position);

            _position = position;
            _x = position.X;
            _y = position.Y;
        }

        protected override bool CheckModified()
        {
            return !(_x == _position.X
                && _y == _position.Y);
        }

        protected override async Task SaveChangesCoreAsync()
        {
            _position.X = _x;
            _position.Y = _y;
        }

        protected override void CancelChangesCore()
        {
            X = _position.X;
            Y = _position.Y;
        }
    }
}
