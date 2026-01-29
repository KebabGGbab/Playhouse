using CommunityToolkit.Mvvm.ComponentModel;
using Playhouse.Core.Models;

namespace Playhouse.ViewModels.ViewModels
{
    public sealed class BrowserProfileViewModel : ObservableObject
    {
        private readonly BrowserProfile _profile;

        public int Id
        {
            get => _profile.Id;
        }

        public string Name
        {
            get => _profile.Name;
            set => SetProperty(_profile.Name, value, _profile, (m, v) => m.Name = v);
        }

        public BrowserProfileViewModel() : this(new BrowserProfile())
        {
        }

        public BrowserProfileViewModel(BrowserProfile profile)
        {
            _profile = profile;
        }
    }
}
