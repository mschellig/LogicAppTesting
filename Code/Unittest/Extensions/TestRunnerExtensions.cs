using LogicAppUnit;
using Testautomation.Unittest.Extensions;

namespace Unittest.Extensions
{
    public static class TestRunnerExtensions
    {
        public static string GetActionResultOutputAsString(this ITestRunner testRunner, string actionName) 
        {
            return testRunner.GetWorkflowActionOutput(actionName).GetBodyFromOutput();
        }

        public static string GetActionResultInputAsString(this ITestRunner testRunner, string actionName)
        {
            return testRunner.GetWorkflowActionInput(actionName).GetBodyFromInput();
        }

        public static string GetWorkflowResultOutputAsString(this ITestRunner testRunner, string actionName)
        {
            return testRunner.GetWorkflowAction(actionName).GetBodyFromInput();
        }
    }
}
