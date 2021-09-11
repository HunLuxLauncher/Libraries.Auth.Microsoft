using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Libraries.Auth.Microsoft.Xbox
{
    public class DisplayClaims
    {
        [JsonPropertyName("xui")]
        public XuiClaims[] XuiClaims { get; set; }

    }
}
