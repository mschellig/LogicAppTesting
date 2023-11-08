using System.Net;
using System.Text.Json;
using FluentAssertions;
using LiveDemo_UnitTests.DTOs;
using LiveDemo_UnitTests.Helpers;
using LiveDemo_UnitTests.Preparations;
using LogicAppUnit;
using LogicAppUnit.Helper;
using Xunit;
using Microsoft.VisualBasic;
using Xunit.Sdk;
using LogicAppUnit.Mocking;
using Microsoft.AspNetCore.Server.HttpSys;

namespace Testautomation.Unittests;

[Collection("logic-testautomation")]
public class LiveDemo_Unittest: WorkflowTestBase
{
    public LiveDemo_Unittest()
    {
        Initialize(@"..\..\..\..\Logic Apps\logic-testautomation", "livedemo");
    }

    [Fact]
    public void LiveDemoTest()
    {
        #region prep
        var triggerbody = new SbTriggerDTO()
        {
            Name = "MaxMustermann"
        };
        #endregion

        // TODO create testrunner
        using ITestRunner testrunner = CreateTestRunner();

        testrunner.AddApiMocks = (request) =>
        {
            HttpResponseMessage mockedResponse = new HttpResponseMessage();
            if (request.RequestUri.AbsolutePath.Contains("/iss-now.json"))
            {
                mockedResponse.RequestMessage = request;
                mockedResponse.StatusCode = HttpStatusCode.OK;
                mockedResponse.Content = ContentHelper.CreateJsonStringContent(IssMockPreparations.CreateAsiaDTO());
            }
            else if (request.RequestUri.AbsolutePath.Contains("/data/reverse-geocode-client"))
            {
                mockedResponse.RequestMessage = request;
                mockedResponse.StatusCode = HttpStatusCode.OK;
                mockedResponse.Content =
                    ContentHelper.CreateJsonStringContent(ReverseGeocodePreparations.CreateAsiaDTO());
            }
            else if (request.RequestUri.AbsolutePath == "/Send_message")
            {
                mockedResponse.RequestMessage = request;
                mockedResponse.StatusCode = HttpStatusCode.OK;
                mockedResponse.Content = ContentHelper.CreatePlainStringContent("Upsert ok");
            }
            return mockedResponse;
        };

        // TODO trigger workflow
        var response = testrunner.TriggerWorkflow(UnittestHelpers.GetServiceBusMessageForTrigger(triggerbody),
            HttpMethod.Post, new Dictionary<string, string>());

        // TODO Test Workflow Response
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);


        // TODO Test ISS Position Call
        testrunner.GetWorkflowActionStatus("HTTP_-_Get_ISS_Position").Should().Be(ActionStatus.Succeeded);
        var isspositionAction = testrunner.GetWorkflowActionOutput("HTTP_-_Get_ISS_Position");
        var isspositionObject = JsonSerializer.Deserialize<IssPosition_DTO>(isspositionAction["body"].ToString());
        isspositionObject.Should().NotBeNull();

        // TODO Test Reverse Geocode Call

        #region Geocode Call

        testrunner.GetWorkflowActionStatus("HTTP_-_Reverse_Geocode").Should().Be(ActionStatus.Succeeded);
        var geocodeAction = testrunner.GetWorkflowActionOutput("HTTP_-_Reverse_Geocode");
        var geocodeResult = JsonSerializer.Deserialize<ReverseGeocode_DTO>(geocodeAction["body"].ToString());
        geocodeResult.Should().NotBeNull();

        #endregion

        // TODO Test Compose valid

        #region Compose

        testrunner.GetWorkflowActionStatus("Compose_-_prepare_for_mapping").Should().Be(ActionStatus.Succeeded);
        var composeAction = testrunner.GetWorkflowActionOutput("Compose_-_prepare_for_mapping");
        var composeResult = JsonSerializer.Deserialize<Compose_DTO>(composeAction.ToString());
        composeResult.Should().NotBeNull();

        #endregion

        // TODO Test liquid

        #region Liquid

        testrunner.GetWorkflowActionStatus("Transform_JSON_To_JSON").Should().Be(ActionStatus.Succeeded);
        var liquidAction = testrunner.GetWorkflowActionOutput("Transform_JSON_To_JSON");
        var liquidResult = JsonSerializer.Deserialize<Liquid_DTO>(liquidAction["body"].ToString());
        liquidResult.Should().NotBeNull();

        liquidResult.Survive.Should().BeTrue();

        #endregion

        // TODO Test Condition
        testrunner.GetWorkflowActionStatus("Condition_-_Survive").Should().Be(ActionStatus.Succeeded);

        //TODO Test Outcome
        testrunner.GetWorkflowActionStatus("Terminate_-_You_Die").Should().Be(ActionStatus.Skipped);
        testrunner.GetWorkflowActionStatus("Send_message").Should().Be(ActionStatus.Succeeded);
    }

}
