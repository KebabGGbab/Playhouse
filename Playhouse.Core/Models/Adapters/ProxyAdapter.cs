using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Playwright;
using Playhouse.Core.Enums;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Playhouse.Core.Models.Adapters
{
    public sealed partial class ProxyAdapter : ObservableObject, IDataErrorInfo
    {
        private readonly Proxy _proxy;

        public string Server
        {
            get => _proxy.Server;
            set
            {
                if (_proxy.Server == value)
                {
                    return;
                }

                _proxy.Server = value;
                OnPropertyChanged();
            }
        }

		public ProxyProtocolType? ProxyType
        {
            get
            {
                if (Server == null)
                {
                    return ProxyProtocolType.none;
                }

                int indexFirstTwoDot = _proxy.Server.IndexOf(':', StringComparison.CurrentCulture);
				string type = _proxy.Server[..indexFirstTwoDot];
                
                if (type.Length == 0)
                {
                    return ProxyProtocolType.none;
                }

				return Enum.Parse<ProxyProtocolType>(type);
            }
            set
            {
                ProxyProtocolType? type = ProxyType;

                if (type == null || type == ProxyProtocolType.none)
                {
                    Server = string.Concat(value);
                }
                else
                {
                    Server = Server.Replace(type.ToString(), value.ToString(), StringComparison.Ordinal);
                }

                OnPropertyChanged();
			}
        }

		public string? Address
        {
            get
            {
                if (Server == null)
                {
                    return string.Empty;
                }

                return _proxy.Server[(_proxy.Server.LastIndexOf('/') + 1)..];
            }
            set
            {
                int indexLastSlash = _proxy.Server.LastIndexOf('/');
                string serverWithoutAddress = _proxy.Server[..(indexLastSlash + 1)];

			    _proxy!.Server = string.Concat(serverWithoutAddress, value);

                OnPropertyChanged(nameof(Address));
			}
        }

		public string? Bypass
        {
            get => _proxy.Bypass;
            set
            {
				if (_proxy.Bypass == value)
                {
                    return;
                }

                _proxy.Bypass = value;
                OnPropertyChanged();
			}
        }

		public string? Username
        {
            get => _proxy.Username;
            set
            {
                if (_proxy.Username == value)
                {
                    return;
                }

                _proxy.Username = value;
                OnPropertyChanged();
			}
        }

		public string? Password
        {
            get => _proxy.Password;
            set
            {
				if (_proxy.Password == value)
                {
                    return;
                }

                _proxy.Password = value;
                OnPropertyChanged();
			}
        }

		[JsonIgnore]
		public string Error => string.Empty;

        public string this[string propertyName]
        {
            get
            {
                string error = string.Empty;

                switch (propertyName)
                {
                    case nameof(Address):
                    {
                        if (!string.IsNullOrEmpty(Address))
                        {
                            if (ProxyType == null)
                            {
                                error = "Выберите тип прокси-сервера.";
                            }

                            Match match = AddressRegex().Match(Address);

                            if (match.Success)
                            {
                                string[] proxyAddress = match.Groups["host"].Value.Split('.');
                                int proxyPort = int.Parse(match.Groups["port"].Value, NumberStyles.Integer,  CultureInfo.CurrentCulture.NumberFormat);

                                foreach (string partAddress in proxyAddress)
                                {
                                    int part = int.Parse(partAddress, NumberStyles.Integer, CultureInfo.CurrentCulture.NumberFormat);

                                    if (!(part >= 0 && part <= 255))
                                    {
                                        if (!string.IsNullOrEmpty(error))
                                        {
                                            error += Environment.NewLine;
                                        }

                                        error += string.Concat("Октет должен находиться в диапазоне {0; 255}. Ошибка в октете ", part, ".");
                                    }
                                }

                                if (!(proxyPort >= 0 && proxyPort <= 65535))
                                {
                                    if (!string.IsNullOrEmpty(error))
                                    {
                                        error += Environment.NewLine;
                                    }

                                    error += "Порт должен находиться в диапазоне {0; 65535}.";
                                }
                            }
                            else
                            {
                                error = string.Concat("Хост должен иметь формат 'Адрес:Порт'.", Environment.NewLine, "Каждый октет адреса хоста должен находиться в диапазоне {0; 255}.", Environment.NewLine, "Порт хоста должен находиться в диапазоне {0; 65535}.", Environment.NewLine, "Например: '255.192.32.10:9234'.");
                            }
                        }
                        break;
                    }
                    case nameof(Bypass):
                    {
                        if (!string.IsNullOrEmpty(Bypass))
                        {
                            if (string.IsNullOrEmpty(Address) || ProxyType == null)
                            {
                                error = "Не заданы прокси-сервер и его тип.";
                            }

                            string[] domens = Bypass.Split(", ");

                            foreach (string domen in domens)
                            {
                                Match match = ProxyBypassRegex().Match(domen);

                                if (!match.Success)
                                {
                                    if (!string.IsNullOrEmpty(error))
                                    {
                                        error += Environment.NewLine;
                                    }

                                    error += string.Concat("Ошибка в доменном имене: '", domen, "'. Доменные имена должны отделяться друг от друга запятой с пробелом ', '.", Environment.NewLine, "Например: '.com, chromium.org, .domain.com'.");
                                }
                            }
                        }
                        break;
                    }
                    case nameof(Username):
                    {
                        if (!string.IsNullOrEmpty(Username) && (string.IsNullOrWhiteSpace(Address) || ProxyType == null))
                        {
                            error = "Нельзя задать логин для авторизации прокси сервера без типа прокси или прокси сервера.";
                        }

                        break;
                    }
                    case nameof(Password):
                    {
                        if (!string.IsNullOrEmpty(Password) && (string.IsNullOrWhiteSpace(Address) || ProxyType == null))
                        {
                            error = "Нельзя задать пароль для авторизации прокси сервера без типа прокси или прокси сервера.";
                        }

                        break;
                    }
                    case nameof(ProxyType):
                    {
                        if (ProxyType != null && string.IsNullOrEmpty(Address) && _proxy != null && ProxyType != ProxyProtocolType.none)
                        {
                            error = "Нельзя задать тип прокси сервера без прокси сервера.";
                        }

                        break;
                    }
                }

                return error;
            }
        }

        public ProxyAdapter(Proxy proxy)
        {
            ArgumentNullException.ThrowIfNull(proxy, nameof(proxy));

            _proxy = proxy;
        }

        [GeneratedRegex(@"^(?<host>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}):(?<port>\d{1,5})$", RegexOptions.Singleline | RegexOptions.ExplicitCapture, 2000)]
        private static partial Regex AddressRegex();

		[GeneratedRegex(@"^(?<type>http|socks)://(?<address>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}):(?<port>\d{1,5})$", RegexOptions.Singleline | RegexOptions.ExplicitCapture, 2000)]
        private static partial Regex ProxyServerRegex();

        [GeneratedRegex(@"^(?:[a-zA-Z0-9-_.]+)*\.[a-zA-Z]{2,}$", RegexOptions.Singleline | RegexOptions.ExplicitCapture, 10000)]
        private static partial Regex ProxyBypassRegex();
    }
}