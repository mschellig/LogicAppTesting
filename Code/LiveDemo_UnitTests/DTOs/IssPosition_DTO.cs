using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LiveDemo_UnitTests.DTOs
{
    public class IssPosition_DTO
    {
        [JsonProperty("iss_position")]
        [JsonPropertyName("iss_position")]
        public IssPosition_SubDTO IssPosition { get; set; }
    }

    public class IssPosition_SubDTO
    {
        [JsonProperty("longitude")]
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
        [JsonProperty("latitude")]
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
    }
}
