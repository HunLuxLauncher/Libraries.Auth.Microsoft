using System.Text.Json.Serialization;

namespace Libraries.Auth.Microsoft.Minecraft
{
    internal class AuthenticateRequest
    {
        [JsonPropertyName("identityToken")]
        public string IdentityToken { get; set; }
    }
}