using System.Text.Json.Serialization;

namespace Libraries.Auth.Microsoft.Minecraft
{
    public class OwnedItem
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("signature")]
        public string Signature { get; set; }

    }
}