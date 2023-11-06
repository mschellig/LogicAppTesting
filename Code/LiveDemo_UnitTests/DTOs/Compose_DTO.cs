using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LiveDemo_UnitTests.DTOs
{
    public class Compose_DTO
    {
        [JsonProperty("iss")]
        [JsonPropertyName("iss")]
        public ComposeIssModel IssModel { get; set; }
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name{ get; set; }
        [JsonProperty("world")]
        [JsonPropertyName("world")]
        public ComposeWorldModel WorldModel { get; set; }
    }

    public class ComposeIssModel
    {
        [JsonProperty("latitude")]
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
        [JsonProperty("longitude")]
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
    }

    public class ComposeWorldModel
    {
        [JsonProperty("city")]
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonProperty("continent")]
        [JsonPropertyName("continent")]
        public string Continent { get; set; }
        [JsonProperty("latitude")]
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
        [JsonProperty("longitutde")]
        [JsonPropertyName("longitutde")]
        public string Longitude { get; set; }
        [JsonProperty("principalSubdivision")]
        [JsonPropertyName("principalSubdivision")]
        public string PrincipalSubdivision { get; set; }
    }

}
