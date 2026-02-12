using CommunityToolkit.Mvvm.ComponentModel;
using Playhouse.Core.Models;

namespace Playhouse.ViewModels.ViewModels
{
    public class BrowserProfileViewModel : ObservableObject
    {
        internal BrowserProfile Profile { get; }

        public int Id => Profile.Id;

        public string Name => Profile.Name;

        public bool AcceptDownloads => Profile.Options.AcceptDownloads;

        public string? Channel => Profile.Options.Channel;

        public bool ChromiumSandbox => Profile.Options.ChromiumSandbox;

        public string? DownloadsPath => Profile.Options.DownloadsPath;

        public bool Headless => Profile.Options.Headless;

        public BrowserProfileViewModel() : this(new BrowserProfile())
        {
        }

        public BrowserProfileViewModel(BrowserProfile profile)
        {
            Profile = profile;
        }
    }
}
