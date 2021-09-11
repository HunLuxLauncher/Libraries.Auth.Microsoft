using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Libraries.Auth.Microsoft.Xbox
{
    public class AuthenticationRequest
    {
#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        public AuthenticationProperties Properties { get; set; }
#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        public string RelyingParty { get; set; }
#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
#endif
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TokenType? TokenType { get; set; }
    }
}
