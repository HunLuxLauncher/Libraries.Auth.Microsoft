using System.Text.Json;

namespace Libraries.Auth.Microsoft
{
    internal class Globals
    {
        public static JsonSerializerOptions JsonSerializerOptions
        {
            get => new()
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
#if NET5_0_OR_GREATER
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
#endif
                ReadCommentHandling = JsonCommentHandling.Skip
            };
        }
    }
}