using System.Text.Json.Serialization;
using Microsoft.Playwright;
using Playhouse.Domain.BotActions.Abstractions;

namespace Playhouse.Application.Services.ConstructorService
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
            if (Enum.TryParse(Role, true, out AriaRole role) == false || Enum.IsDefined(role) == false)
            {
                role = AriaRole.None;
            }

            ActionTypes.TryFromName(Action, true, out ActionTypes action);

            return new(action, Selector!, role, Id, Text);
        }
    }
}
