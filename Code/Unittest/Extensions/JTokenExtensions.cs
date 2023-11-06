using Newtonsoft.Json.Linq;

namespace Testautomation.Unittest.Extensions
{
    internal static class JTokenExtensions
    {
        public static string GetBodyFromInput(this JToken input)
        {
            return input["content"].ToString();
        }
        public static string GetBodyFromOutput(this JToken input)
        {
            return input["body"].ToString();
        }
    }

}
