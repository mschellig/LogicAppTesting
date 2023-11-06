using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LiveDemo_UnitTests.DTOs
{
    public class SbTriggerDTO
    {
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
