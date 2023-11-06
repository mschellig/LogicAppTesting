using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LiveDemo_UnitTests.DTOs
{
    public class ReverseGeocode_DTO
    {
        [JsonProperty("latitude")]
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("longitude")]
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
        [JsonProperty("continent")]
        [JsonPropertyName("continent")]
        public string Continent { get; set; }
        [JsonProperty("city")]
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonProperty("principalSubdivision")]
        [JsonPropertyName("principalSubdivision")]
        public string PincipalSubdivision { get; set; }
    }
}
