using System.Text.Json.Serialization;

namespace Testautomation.Unittest.Models
{
    public class SimpleWorkflow_CityInformation_Model
    {
        [JsonPropertyName("post code")]
        public string PostCode { get; set; } = string.Empty;
        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;
        [JsonPropertyName("country abbreviation")]
        public string CountryAbbreviation { get; set; } = string.Empty;
        [JsonPropertyName("places")]
        public List<Places> Places { get; set; } = new List<Places>();
    }

    public class Places
    {
        [JsonPropertyName("place name")]
        public string PlaceName { get; set; } = string.Empty;
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; } = string.Empty;
        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;
        [JsonPropertyName("state abbreviation")]
        public string StateAbbreviation { get; set; } = string.Empty;
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; } = string.Empty;
    }
}
