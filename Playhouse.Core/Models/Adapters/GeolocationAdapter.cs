using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Playwright;
using System.ComponentModel;

namespace Playhouse.Core.Models.Adapters
{
    public sealed class GeolocationAdapter : ObservableObject, IDataErrorInfo
    {
        private readonly Geolocation _geolocation;

        public float Latitude
        {
            get => _geolocation.Latitude;
            set => SetProperty(_geolocation.Latitude, value, _geolocation, (m, v) => m.Latitude = v);
        }

        public float Longitude
        {
            get => _geolocation.Longitude;
            set => SetProperty(_geolocation.Longitude, value, _geolocation, (m, v) => m.Longitude = v);
        }

        public float? Accuracy
        {
            get => _geolocation.Accuracy;
            set => SetProperty(_geolocation.Accuracy, value, _geolocation, (m, v) => m.Accuracy = v);
        }

        public string Error => string.Empty;

        private bool? isReady;
        public string this[string propertyName]
        {
            get
            {
                string error = string.Empty;
                switch (propertyName)
                {
                    case nameof(Latitude):
                    case nameof(Longitude):
                        {
                            if (Latitude == 0F && Longitude != 0F)
                            {
                                error = "При указании долготы, необходимо указать широту.";
                                isReady = false;
                            }
                            if (Latitude != 0F && Longitude == 0F)
                            {
                                error = "При указании широты, необходимо указать долготу.";
                                isReady = false;
                            }
                            else if (isReady == false && (Latitude != 0F && Longitude != 0F || Latitude == 0F && Longitude == 0F))
                            {
                                isReady = true;
                                OnPropertyChanged(nameof(Latitude));
                                OnPropertyChanged(nameof(Longitude));
                            }
                            break;
                        }
                }
                return error;
            }
        }

        public GeolocationAdapter(Geolocation geolocation)
        {
            ArgumentNullException.ThrowIfNull(geolocation, nameof(geolocation));

            _geolocation = geolocation;
            isReady = Latitude != 0F && Longitude != 0F || Latitude == 0F && Longitude == 0F;
        }
    }
}