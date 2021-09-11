using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Libraries.Auth.Microsoft.Xbox
{
    public class AuthenticationProperties
    {
#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
#endif
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AuthMethod? AuthMethod { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        public string SiteName { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        public string RpsTicket { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        public string SandboxId { get; set; }

#if NET5_0
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
#endif
        public List<string> UserTokens { get; set; }
    }
}
