using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Playwright;

namespace Playhouse.Core.Models.Adapters
{
    public sealed class RecordVideoSizeAdapters : ObservableObject
    {
        private readonly RecordVideoSize _recordVideoSize;

        public int Width
        {
            get => _recordVideoSize.Width == 0 ? 1980 : _recordVideoSize.Width;
            set => SetProperty(_recordVideoSize.Width, value, _recordVideoSize, (m, v) => m.Width = v);
        }

        public int Height
        {
            get => _recordVideoSize.Height == 0 ? 1080 : _recordVideoSize.Height;
            set => SetProperty(_recordVideoSize.Height, value, _recordVideoSize, (m, v) => m.Height = v);
        }

        public RecordVideoSizeAdapters(RecordVideoSize recordVideoSize)
        {
            ArgumentNullException.ThrowIfNull(recordVideoSize, nameof(recordVideoSize));

            _recordVideoSize = recordVideoSize;
        }
    }
}
