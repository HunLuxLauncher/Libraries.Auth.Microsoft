using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Libraries.Auth.Microsoft.Converter;

namespace Libraries.Auth.Microsoft.Minecraft
{
    public class ProfileInfo
    {

        [JsonPropertyName("id")]
        [JsonConverter(typeof(JsonStringGuidConverter))]
        public Guid Uuid { get; set; }

        [JsonPropertyName("name")]
        public string Username { get; set; }

        [JsonPropertyName("skins")]
        public List<TextureInfo> Skins { get; set; }
        
        [JsonPropertyName("capes")]
        public List<TextureInfo> Capes { get; set; }

    }
}