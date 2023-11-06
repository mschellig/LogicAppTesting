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


        // TODO trigger workflow

        // TODO Test Workflow Response

        // TODO Test ISS Position Call

        // TODO Test Reverse Geocode Call
        #region Geocode Call
        //testrunner.GetWorkflowActionStatus("HTTP_-_Reverse_Geocode").Should().Be(ActionStatus.Succeeded);
        //var geocodeAction = testrunner.GetWorkflowActionOutput("HTTP_-_Reverse_Geocode");
        //var geocodeResult = JsonSerializer.Deserialize<ReverseGeocode_DTO>(geocodeAction["body"].ToString());
        //geocodeResult.Should().NotBeNull();
        #endregion

        // TODO Test Compose valid
        #region Compose 
        //testrunner.GetWorkflowActionStatus("Compose_-_prepare_for_mapping").Should().Be(ActionStatus.Succeeded);
        //var composeAction = testrunner.GetWorkflowActionOutput("Compose_-_prepare_for_mapping");
        //var composeResult = JsonSerializer.Deserialize<Compose_DTO>(composeAction.ToString());
        //composeResult.Should().NotBeNull();
        #endregion

        // TODO Test liquid
        #region Liquid
        //testrunner.GetWorkflowActionStatus("Transform_JSON_To_JSON").Should().Be(ActionStatus.Succeeded);
        //var liquidAction = testrunner.GetWorkflowActionOutput("Transform_JSON_To_JSON");
        //var liquidResult = JsonSerializer.Deserialize<Liquid_DTO>(liquidAction["body"].ToString());
        //liquidResult.Should().NotBeNull();

        #endregion

        // TODO Test Condition

        //TODO Test Outcome
    }

}
