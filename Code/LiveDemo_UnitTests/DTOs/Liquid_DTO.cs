using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LiveDemo_UnitTests.DTOs
{
    public class Liquid_DTO
    {
        [JsonProperty("longitude")]
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
        [JsonProperty("latitude")]
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
        [JsonProperty("place")]
        [JsonPropertyName("place")]
        public string Place { get; set; }
        [JsonProperty("result")]
        [JsonPropertyName("result")]
        public string Result { get; set; }
        [JsonProperty("survive")]
        [JsonPropertyName("survive")]
        public bool Survive { get; set; }
    }
}
