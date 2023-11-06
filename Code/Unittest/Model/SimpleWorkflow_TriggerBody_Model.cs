namespace Testautomation.Unittest.Models
{
    public class SimpleWorkflow_TriggerBody_Model
    {
        public string name1 { get; set; } = string.Empty;
        public string name2 { get; set; } = string.Empty;
        public string birthdate { get; set; } = string.Empty;
        public int salary_year { get; set; } = 0;
        public int postalcode { get; set; } = 0;
        public SimpleWorkflow_TriggerBodyHelpers_Model ExpectationHelpers { get; set; }
    }
    public class SimpleWorkflow_TriggerBodyHelpers_Model
    {
        public string ExpectedState { get; set; }
        public string ExpectedPlace { get; set; }
    }
}
