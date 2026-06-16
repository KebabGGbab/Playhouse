using System.Text.Json.Serialization;

namespace Playhouse.Application.Services.ConstructorService
{
    internal sealed class PageActionDataDto
    {
        [JsonPropertyName("referrer")]
        public string? Referrer { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }
    }
}
