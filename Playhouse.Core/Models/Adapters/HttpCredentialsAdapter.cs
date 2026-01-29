using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Playwright;

namespace Playhouse.Core.Models.Adapters
{
    public sealed class HttpCredentialsAdapter : ObservableObject
    {
        private readonly HttpCredentials _httpCredentials;

        public string Username
        {
            get => _httpCredentials.Username;
            set => SetProperty(_httpCredentials.Username, value, _httpCredentials, (m, v) => m.Username = v);
        }

        public string Password
        {
            get =>  _httpCredentials.Password;
            set => SetProperty(_httpCredentials.Password, value, _httpCredentials, (m, v) => m.Password = v);
        }

        public string? Origin
        {
            get => _httpCredentials.Origin;
            set => SetProperty(_httpCredentials.Origin, value, _httpCredentials, (m, v) => m.Origin = v);
        }

        public HttpCredentialsSend? Send
        {
            get => _httpCredentials.Send;
            set => SetProperty(_httpCredentials.Send, value, _httpCredentials, (m, v) => m.Send = v);
        }

        public HttpCredentialsAdapter(HttpCredentials httpCredentials)
        {
            ArgumentNullException.ThrowIfNull(httpCredentials, nameof(httpCredentials));

            _httpCredentials = httpCredentials;
        }
    }
}