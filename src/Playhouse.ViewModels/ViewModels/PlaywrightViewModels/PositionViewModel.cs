using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Playwright;
using Playhouse.ViewModels.EventArguments;

namespace Playhouse.ViewModels.ViewModels.PlaywrightViewModels
{
    public class PositionViewModel : ObservableObject
    {
        private Position? _position;

        public float X
        {
            get => _position == null ? 0 : _position.X;
            set
            {
                if (_position == null)
                {
                    throw new InvalidOperationException();
                }

                SetProperty(_position.X, value, _position, (m, v) => m.X = v);
            }
        }

        public float Y
        {
            get => _position == null ? 0 : _position.Y;
            set
            {
                if (_position == null)
                {
                    throw new InvalidOperationException();
                }

                SetProperty(_position.Y, value, _position, (m, v) => m.Y = v);
            }
        }

        public bool IsRandom
        {
            get => _position == null;
            set => SetProperty(IsRandom, value, (v) =>
            {
                if (v)
                {
                    _position = null;
                    OnPropertyChanged(string.Empty);
                }
                else
                {
                    _position = new Position();
                }

                OnPositionChanged(_position);
            });
        }

        public event EventHandler<PositionViewModel, PositionChangedEventArgs>? PositionChanged;

        public PositionViewModel(Position? position = null)
        {
            ArgumentNullException.ThrowIfNull(position, nameof(position));

            _position = position;
        }

        private void OnPositionChanged(Position? newPosition)
        {
            PositionChanged?.Invoke(this, new PositionChangedEventArgs(newPosition));
        }
    }
}
