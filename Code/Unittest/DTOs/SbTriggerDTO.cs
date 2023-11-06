using System.Text.Json.Serialization;

namespace LiveDemo_UnitTests.DTOs
{
    public class SbTriggerDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
