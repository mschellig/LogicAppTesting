using System.Text.Json.Serialization;

namespace LiveDemo_UnitTests.DTOs
{
    public class IssPosition_DTO
    {
        [JsonPropertyName("iss_position")]
        public IssPosition_SubDTO IssPosition { get; set; }
    }

    public class IssPosition_SubDTO
    {
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
    }
}
