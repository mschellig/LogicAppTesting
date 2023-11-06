using System.Net;
using FluentAssertions;
using LogicAppUnit;
using Testautomation.Unittest;
using Testautomation.Unittest.Models;
using Testautomation.Unittest.Extensions;
using Xunit;

namespace Testautomation.Unittests;

[Collection("logic-testautomation")]
public class InvokeWf_Unittest : WorkflowTestBase
{
    public const string Workflowname = "http-invokewf";

    public InvokeWf_Unittest()
    {
        Initialize(Constants.LogicAppBasePath, Workflowname);
    }

    [Fact]
    public void happypath()
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
        // Act
        HttpResponseMessage? workflowResponse = testRunner.TriggerWorkflow(
            triggerbody.GetRequestFromDto(),
            HttpMethod.Post,
            new Dictionary<string, string>());

        #region Assert
        testRunner.WorkflowRunStatus.Should().Be(WorkflowRunStatus.Succeeded);
        workflowResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        #endregion
        #region check 'Try' scope
        testRunner.GetWorkflowActionStatus("Scope_-_try").Should().Be(ActionStatus.Succeeded);
        #endregion
        #region check response
        testRunner.GetWorkflowActionStatus("Response").Should().Be(ActionStatus.Succeeded);
        testRunner.GetWorkflowActionStatus("Response_-_NOK").Should().Be(ActionStatus.Skipped);
        #endregion
    }

    [Fact]
    public void wrongPostcode()
    {
        #region prep
        var triggerbody = new SimpleWorkflow_TriggerBody_Model()
        {
        };
        #endregion
        using ITestRunner testRunner = CreateTestRunner();
        // Act
        HttpResponseMessage? workflowResponse = testRunner.TriggerWorkflow(
            triggerbody.GetRequestFromDto(),
            HttpMethod.Post,
            new Dictionary<string, string>());

        #region Assert
        testRunner.WorkflowRunStatus.Should().Be(WorkflowRunStatus.Succeeded);
        workflowResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        #endregion
        #region check 'Try' scope
        testRunner.GetWorkflowActionStatus("Scope_-_try").Should().Be(ActionStatus.Succeeded);
        #endregion
        #region check response
        testRunner.GetWorkflowActionStatus("Response").Should().Be(ActionStatus.Succeeded);
        testRunner.GetWorkflowActionStatus("Response_-_NOK").Should().Be(ActionStatus.Skipped);
        #endregion
    }
}
