using System.Text.Json.Serialization;
using Microsoft.Playwright;
using Playhouse.Core.Models.BotActions.Abstractions;

namespace Playhouse.Core.Services.ConstructorService
{
    internal sealed class LocatorActionDataDto
    {
        [JsonPropertyName("action")]
        public string? Action { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("selector")]
        public string? Selector { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }

        public LocatorActionData ToLocatorActionData()
        {
            Enum.TryParse(Role, true, out AriaRole role);

            return new(Action!, Selector!, role, Id, Text);
        }
    }
}
