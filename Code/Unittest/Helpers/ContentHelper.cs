using Newtonsoft.Json;
using System.Text;

namespace Unittest.Helpers
{
    public static class ContentHelper
    {
        private const string JsonContentType = "application/json";


        public static StringContent CreateJsonStringContent(object jsonObject)
        {
            // The name of this method is inconsistent
            if (jsonObject == null)
                throw new ArgumentNullException(nameof(jsonObject));

            var json = JsonConvert.SerializeObject(jsonObject);
            return new StringContent(json, Encoding.UTF8, JsonContentType);
        }

    }
}
