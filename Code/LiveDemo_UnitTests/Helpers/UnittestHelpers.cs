using System.Text;
using System.Text.Json;
using LogicAppUnit;
using LogicAppUnit.Helper;

namespace LiveDemo_UnitTests.Helpers
{
    public static class UnittestHelpers
    {
        public static StringContent GetRequestFromDto(object dto)
        {
            JsonSerializerOptions options = new() { IgnoreNullValues = true };
            return ContentHelper.CreateJsonStringContent(JsonSerializer.Serialize(dto, options));
        }

        private const string JsonContentType = "application/json";



        public static HttpContent GetServiceBusMessageForTrigger(object o)
        {
            return ContentHelper.CreateJsonStringContent(new
            {
                // The JSON must match the data structure used by the Service Bus trigger, this includes 'contentData' to represent the message content
                contentData = o,
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

        public static string GetActionResultOutputAsString(this ITestRunner testRunner, string actionName)
        {
            var input = testRunner.GetWorkflowActionOutput(actionName);
            return input["body"].ToString();
        }

        public static T GetActionResultOutputAsObject<T>(this ITestRunner testRunner, string actionName)
        {
            var input = testRunner.GetWorkflowActionOutput(actionName);
            return JsonSerializer.Deserialize<T>(input["body"].ToString());
        }
    }
}
