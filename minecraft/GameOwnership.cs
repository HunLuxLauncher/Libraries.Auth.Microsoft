using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Libraries.Auth.Microsoft.Minecraft
{
    public class GameOwnership
    {
        [JsonPropertyName("items")]
        public List<OwnedItem> Items { get; set; }
        
        [JsonPropertyName("signature")]
        public string Signature { get; set; }

        [JsonPropertyName("keyId")]
        public string KeyId { get; set; }
    }
}