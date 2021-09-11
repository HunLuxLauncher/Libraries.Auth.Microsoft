using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Libraries.Auth.Microsoft.Xbox
{
    public class TokenResponse
    {
        [JsonPropertyName("IssueInstant")]
        public string IssueInstant { get; set; }

        [JsonPropertyName("NotAfter")]
        public string NotAfter { get; set; }

        [JsonPropertyName("Token")]
        public string Token { get; set; }

        [JsonPropertyName("DisplayClaims")]
        public DisplayClaims DisplayClaims { get; set; }

        public byte[] SigningProofKey { get; set; }
    }
}
