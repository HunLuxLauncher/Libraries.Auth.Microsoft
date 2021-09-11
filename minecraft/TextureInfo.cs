using System;
using System.Text.Json.Serialization;

namespace Libraries.Auth.Microsoft.Minecraft
{
    public class TextureInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// <b>WARNING<br/></b>This value might be enum after everyone can migrate their accounts to Microsoft.net account <i>(because this API is written using the <b>wiki.vg</b> article only)</i>.
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("url")]
        public Uri Url { get; set; }

        /// <summary>
        /// <b>WARNING<br/></b>This value might be enum after everyone can migrate their accounts to Microsoft.net account <i>(because this API is written using the <b>wiki.vg</b> article only)</i>.
        /// </summary>
        [JsonPropertyName("variant")]
        public string Variant { get; set; }
        
        /// <summary>
        /// <b>WARNING<br/></b>This value might be enum after everyone can migrate their accounts to Microsoft.net account <i>(because this API is written using the <b>wiki.vg</b> article only)</i>.
        /// </summary>
        [JsonPropertyName("alias")]
        public string Alias { get; set; }

    }
}