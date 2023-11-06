using FluentAssertions;
using LogicAppUnit;
using LogicAppUnit.Helper;
using Testautomation.Unittest;
using Testautomation.Unittest.Models;
using Xunit;

namespace Testautomation.Unittests;

[Collection("logic-testautomation")]
public class SbInvokeWf_Unittest : WorkflowTestBase
{
    public const string Workflowname = "sb-invokewf";

    public SbInvokeWf_Unittest()
    {
        Initialize(Constants.LogicAppBasePath, Workflowname);
    }

    #region Tests
    [Fact]
    public void BuiltInConnectorWorkflowTest_When_Invalid_Language_Code()
    {
        #region prep
        var triggerbody = new SimpleWorkflow_TriggerBody_Model()
        {
            name1 = "Max",
            name2 = "Mustermann",
            birthdate = "01.01.2000",
            salary_year = 120000,
            postalcode = 70736,
            ExpectationHelpers = new()
            {
                ExpectedPlace = "Fellbach",
                ExpectedState = "Baden-Württemberg"
            }
        };
        #endregion

        using ITestRunner testRunner = CreateTestRunner();
        // The Service Bus trigger has been replaced with a HTTP trigger that uses a POST method
        testRunner.TriggerWorkflow(GetServiceBusMessageForTriggerWithInvalidLanguageCode(), HttpMethod.Post);

        #region Asserts
        testRunner.WorkflowRunStatus.Should().Be(WorkflowRunStatus.Succeeded);
        testRunner.WorkflowRunStatus.Should().Be(WorkflowRunStatus.Succeeded);
        testRunner.GetWorkflowActionStatus("Scope_-_try").Should().Be(ActionStatus.Succeeded);
        testRunner.GetWorkflowActionStatus("Terminate").Should().Be(ActionStatus.Succeeded);
        testRunner.GetWorkflowActionStatus("Terminate_-_fail").Should().Be(ActionStatus.Skipped);
        #endregion
    }
    #endregion
    #region Helpers
    private static HttpContent GetServiceBusMessageForTriggerWithInvalidLanguageCode()
    {
        return ContentHelper.CreateJsonStringContent(new
        {
            // The JSON must match the data structure used by the Service Bus trigger, this includes 'contentData' to represent the message content
            contentData = new
            {
                name1 = "Max",
                name2 = "Mustermann",
                birthdate = "01.01.2000",
                salary_year = 120000,
                postalcode = 70736
            },
            contentType = "application/json",
            messageId = "ff421d65-5be6-4084-b748-af490100c9a5",
            label = "customer.54624",
            scheduledEnqueueTimeUtc = "1/1/0001 12:00:00 AM",
            sessionId = "54624",
            timeToLive = "06:00:00",
            deliveryCount = 1,
            enqueuedSequenceNumber = 6825,
            enqueuedTimeUtc = "2022-11-10T15:34:57.727Z",
            lockedUntilUtc = "9999-12-31T23:59:59.9999999Z",
            lockToken = "056bb9fa-9b8f-4d93-874b-7e78e71a588d",
            sequenceNumber = 980
        });
    }
    #endregion
}
