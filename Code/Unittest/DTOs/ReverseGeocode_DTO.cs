using System.Text.Json.Serialization;

namespace LiveDemo_UnitTests.DTOs
{
    public class ReverseGeocode_DTO
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
        [JsonPropertyName("continent")]
        public string Continent { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("principalSubdivision")]
        public string PincipalSubdivision { get; set; }
    }
}
