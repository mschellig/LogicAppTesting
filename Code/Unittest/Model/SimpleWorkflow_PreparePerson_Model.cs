using System.Text.Json.Serialization;

namespace Testautomation.Unittest.Models
{
    public class SimpleWorkflow_PreparePerson_Model
    {
        [JsonPropertyName("fullname")]
        public string FullName { get; set; } = string.Empty;
        [JsonPropertyName("dob")]
        public string DateOfBirth { get; set; } = string.Empty;
        [JsonPropertyName("salary_month")]
        public double SalaryMonth { get; set; } = 0d;
        [JsonPropertyName("postalcode")]
        public string PostalCode { get; set; } = string.Empty;
        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;
        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;
    }
}
