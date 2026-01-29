using System.Globalization;
using System.Text.Json.Serialization;

namespace Playhouse.Core.Models.ConfigurationOptions
{
    public class CultureOptions
    {
        [JsonIgnore]
        public CultureInfo Culture { get; private set; } = null!;

        public string Name 
        {
            get => Culture.Name;
            set => Culture = CultureInfo.GetCultureInfo(value);
        } 

        public bool IsSelected { get; set; }

        public CultureOptions(string name)
        {
            Name = name;
        }
    }
}
