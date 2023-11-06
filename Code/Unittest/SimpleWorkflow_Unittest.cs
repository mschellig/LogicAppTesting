using System.Net;
using System.Text.Json;
using FluentAssertions;
using LogicAppUnit;
using LogicAppUnit.Helper;
using LogicAppUnit.Mocking;
using Testautomation.Unittest;
using Testautomation.Unittest.Models;
using Testautomation.Unittest.Extensions;
using Xunit;
using Unittest.Extensions;

namespace Testautomation.Unittests;

[Collection("logic-testautomation")]
public class SimpleWorkflow_Unittest : WorkflowTestBase
{
    public const string Workflowname = "http-simpleworkflow";
    private JsonSerializerOptions _options = new() { IgnoreNullValues = true };

    public SimpleWorkflow_Unittest()
    {
        Initialize(Constants.LogicAppBasePath, Workflowname);
    }

    [Fact]
    public void NoMockAvailableFail()
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

        // Assert
        testRunner.WorkflowRunStatus.Should().Be(WorkflowRunStatus.Succeeded);
        workflowResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // check 'Try' scope
        testRunner.GetWorkflowActionStatus("Scope_-_try").Should().Be(ActionStatus.Succeeded);

        // check initialize map replacer
        testRunner.GetWorkflowActionStatus("HTTP_-_get_city_information").Should().Be(ActionStatus.Succeeded);
        string cityInformationOutput = testRunner.GetActionResultOutputAsString("HTTP_-_get_city_information");
        var expectedCityInformationOutput = JsonSerializer.Deserialize<SimpleWorkflow_CityInformation_Model>(cityInformationOutput, _options);

        // check post api information
        expectedCityInformationOutput.Should().NotBeNull();
        expectedCityInformationOutput.PostCode.Should().Be(triggerbody.postalcode.ToString());
        expectedCityInformationOutput.Places.First().PlaceName.Should().Be(triggerbody.ExpectationHelpers.ExpectedPlace);
        expectedCityInformationOutput.Places.First().State.Should().Be(triggerbody.ExpectationHelpers.ExpectedState);

        // check Prepare Content for 
        testRunner.GetWorkflowActionStatus("Compose_-_prepare_information").Should().Be(ActionStatus.Succeeded);

        // check liquid map
        testRunner.GetWorkflowActionStatus("Transform_JSON_To_JSON_-_prepare_person").Should().Be(ActionStatus.Succeeded);
        string liquidOutput = testRunner.GetActionResultOutputAsString("Transform_JSON_To_JSON_-_prepare_person");
        var expectedLiquidOutput = JsonSerializer.Deserialize<SimpleWorkflow_PreparePerson_Model>(liquidOutput, _options);

        expectedLiquidOutput.Should().NotBeNull();
        expectedLiquidOutput.FullName.Should().Be($"{triggerbody.name1} {triggerbody.name2}");
        expectedLiquidOutput.DateOfBirth.Should().Be(triggerbody.birthdate);
        expectedLiquidOutput.SalaryMonth.Should().Be(triggerbody.salary_year / 12);
        expectedLiquidOutput.PostalCode.Should().Be(triggerbody.postalcode.ToString());
        expectedLiquidOutput.City.Should().Be(expectedCityInformationOutput.Places.First().PlaceName);
        expectedLiquidOutput.State.Should().Be(expectedCityInformationOutput.Places.First().State);

        // check response
        testRunner.GetWorkflowActionStatus("Response").Should().Be(ActionStatus.Succeeded);
        testRunner.GetWorkflowActionStatus("Response_-_NOK").Should().Be(ActionStatus.Skipped);
    }

    [Fact]
    public void MockApi()
    {
        #region prep
        var triggerbody = new SimpleWorkflow_TriggerBody_Model()
        {
            name1 = "Max",
            name2 = "Mustermann",
            birthdate = "01.01.2000",
            salary_year = 120000,
            postalcode = 12345,
            ExpectationHelpers = new()
            {
                ExpectedPlace = "Musterstadt",
                ExpectedState = "Muster-Bundesland"
            }
        };
        #endregion

        using (ITestRunner testRunner = CreateTestRunner())
        {
            testRunner
                .AddMockResponse(
                    MockRequestMatcher.Create()
                        .UsingGet()
                        .WithPath(PathMatchType.Contains, "/de/"))
                .RespondWith(
                    MockResponseBuilder.Create()
                        .WithSuccess()
                        .WithContent(() => GetPostalCodeMockRespnse()));

            HttpResponseMessage? workflowResponse = testRunner.TriggerWorkflow(
                triggerbody.GetRequestFromDto(),
                HttpMethod.Post,
                new Dictionary<string, string>());

            #region check initialize map replacer
            testRunner.GetWorkflowActionStatus("HTTP_-_get_city_information").Should().Be(ActionStatus.Succeeded);
            string cityInformationOutput = testRunner.GetActionResultOutputAsString("HTTP_-_get_city_information");
            var expectedCityInformationOutput = JsonSerializer.Deserialize<SimpleWorkflow_CityInformation_Model>(cityInformationOutput, _options);
            #endregion
            #region check post api information
            expectedCityInformationOutput.Should().NotBeNull();
            expectedCityInformationOutput.PostCode.Should().Be(triggerbody.postalcode.ToString());
            expectedCityInformationOutput.Places.First().PlaceName.Should().Be(triggerbody.ExpectationHelpers.ExpectedPlace);
            expectedCityInformationOutput.Places.First().State.Should().Be(triggerbody.ExpectationHelpers.ExpectedState);
            #endregion
            #region check Prepare Content for 
            testRunner.GetWorkflowActionStatus("Compose_-_prepare_information").Should().Be(ActionStatus.Succeeded);
            #endregion
            #region check liquid map
            testRunner.GetWorkflowActionStatus("Transform_JSON_To_JSON_-_prepare_person").Should().Be(ActionStatus.Succeeded);
            string liquidOutput = testRunner.GetActionResultOutputAsString("Transform_JSON_To_JSON_-_prepare_person");
            var expectedLiquidOutput = JsonSerializer.Deserialize<SimpleWorkflow_PreparePerson_Model>(liquidOutput, _options);

            expectedLiquidOutput.Should().NotBeNull();
            expectedLiquidOutput.FullName.Should().Be($"{triggerbody.name1} {triggerbody.name2}");
            expectedLiquidOutput.DateOfBirth.Should().Be(triggerbody.birthdate);
            expectedLiquidOutput.SalaryMonth.Should().Be(triggerbody.salary_year / 12);
            expectedLiquidOutput.PostalCode.Should().Be(triggerbody.postalcode.ToString());
            expectedLiquidOutput.City.Should().Be(expectedCityInformationOutput.Places.First().PlaceName);
            expectedLiquidOutput.State.Should().Be(expectedCityInformationOutput.Places.First().State);
            #endregion
            #region check response
            testRunner.GetWorkflowActionStatus("Response").Should().Be(ActionStatus.Succeeded);
            testRunner.GetWorkflowActionStatus("Response_-_NOK").Should().Be(ActionStatus.Skipped);
            #endregion
        }
    }


    [Fact]
    public void MockConnector()
    {
        #region prep
        var triggerbody = new SimpleWorkflow_TriggerBody_Model()
        {
            name1 = "Max",
            name2 = "Mustermann",
            birthdate = "01.01.2000",
            salary_year = 120000,
            postalcode = 123,
            ExpectationHelpers = new()
            {
                ExpectedPlace = "",
                ExpectedState = ""
            }
        };
        #endregion
        using (ITestRunner testRunner = CreateTestRunner())
        {
            // Mock the HTTP calls and customize responses
            testRunner.AddApiMocks = (request) =>
            {
                HttpResponseMessage mockedResponse = new HttpResponseMessage();
                if (request.RequestUri.AbsolutePath.Contains("/de/"))
                {
                    mockedResponse.RequestMessage = request;
                    mockedResponse.StatusCode = HttpStatusCode.OK;
                    mockedResponse.Content = GetPostalCodeMockRespnse();
                }
                if (request.RequestUri.AbsolutePath == "/Send_message")
                {
                    mockedResponse.RequestMessage = request;
                    mockedResponse.StatusCode = HttpStatusCode.BadRequest;
                    mockedResponse.Content = ContentHelper.CreatePlainStringContent("Upsert failed");
                }
                return mockedResponse;
            };

            // Act
            HttpResponseMessage? workflowResponse = testRunner.TriggerWorkflow(
                triggerbody.GetRequestFromDto(),
                HttpMethod.Post,
                new Dictionary<string, string>());

            #region do tests
            #endregion
        }
    }

    private static HttpContent GetPostalCodeMockRespnse()
    {
        return ContentHelper.CreateJsonStringContent(JsonSerializer.Serialize(new SimpleWorkflow_CityInformation_Model
        {
            // The JSON must match the data structure used by the Service Bus trigger, this includes 'contentData' to represent the message content
            PostCode = "12345",
            Country = "Musterland",
            CountryAbbreviation = "ML",

            Places = new List<Places>()
            {
                new()
                {
                    PlaceName = "Musterstadt",
                    Longitude = "9.272",
                    State = "Muster-Bundesland",
                    StateAbbreviation = "BW",
                    Latitude = "48.8238"
                }
            }
        }));
    }

    private static StringContent GetMockResponse(string message)
    {
        return ContentHelper.CreateJsonStringContent(new
        {
            message
        });
    }

}