using LogicAppUnit.Helper;
using System.Text.Json;

namespace Testautomation.Unittest.Extensions
{
    public static class TemplateExtensions
    {
        public static StringContent GetRequestFromDto<T>(this T dto)
        {
#pragma warning disable SYSLIB0020
            JsonSerializerOptions options = new JsonSerializerOptions { IgnoreNullValues = true };
#pragma warning restore SYSLIB0020
            string str = JsonSerializer.Serialize(dto, options);
            return ContentHelper.CreateJsonStringContent(str);
        }
    }
}
