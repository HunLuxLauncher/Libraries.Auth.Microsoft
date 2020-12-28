using hu.czompisoftware.libraries.general;
using hu.hunluxlauncher.libraries.auth.microsoft.xbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace hu.hunluxlauncher.libraries.auth.microsoft
{
    public class XboxAuthentication
    {
        private AuthenticationSettings AuthSettings { get; }
        public TokenResponse XboxLive { get; }
        public TokenResponse XboxSecurityTokenService { get; }

        public XboxAuthentication(AuthenticationSettings authSettings, Microsoft.Identity.Client.AuthenticationResult microsoftAccount)
        {
            AuthSettings = authSettings;
            

            XboxLive = XboxLiveAuthenticate(microsoftAccount.AccessToken);
            XboxSecurityTokenService = XSTSAuthenticate(XboxLive.Token);
        }

        #region XboxLiveAuthenticate
        /// <summary>
        /// Authenticate with Xbox Live #1<br/>
        /// Now you're able to authenticate with XBL via your <paramref name="access_token"/>.
        /// </summary>
        /// <param name="access_token">The <b>AccessToken</b> from <see cref="Microsoft.Identity.Client.AuthenticationResult"/>.</param>
        /// <returns></returns>
        private xbox.TokenResponse XboxLiveAuthenticate(string access_token)
        {
            xbox.AuthenticationRequest request = new()
            {
                Properties = new xbox.AuthenticationProperties
                {
                    AuthMethod = xbox.AuthMethod.RPS,
                    SiteName = "user.auth.xboxlive.com",
                    RpsTicket = $"d={access_token}"
                },
                RelyingParty = "http://auth.xboxlive.com",
                TokenType = xbox.TokenType.JWT
            };
            var rstr = JsonSerializer.Serialize(request);
            Logger.Debug(rstr);
            var responseStr = NetHandler.SendPostRequest(AuthLinks.XblAuthLink, AuthSettings.UserAgent, "application/json", rstr, "Accept", "application/json", "x-xbl-contract-version", "1");
            xbox.TokenResponse response = JsonSerializer.Deserialize<xbox.TokenResponse>(responseStr);

            return response;
        }
        #endregion

        #region XSTSAuthenticate
        /// <summary>
        /// Authenticate with Xbox Live #2<br/>
        /// After getting the <see cref="xbox.TokenResponse"/>, you'll be able to authenticate with XSTS.
        /// </summary>
        /// <param name="xbl_token">The <b>Token</b> value of <see cref="xbox.TokenResponse"/>.</param>
        private xbox.TokenResponse XSTSAuthenticate(string xbl_token)
        {
            xbox.AuthenticationRequest request = new()
            {
                Properties = new xbox.AuthenticationProperties
                {
                    SandboxId = "RETAIL",
                    UserTokens = new List<string>()
                    {
                        xbl_token
                    }
                },
                RelyingParty = "rp://api.minecraftservices.com/",
                TokenType = xbox.TokenType.JWT
            };

            xbox.TokenResponse response = JsonSerializer.Deserialize<xbox.TokenResponse>(NetHandler.SendPostRequest(AuthLinks.XstsAuthLink, AuthSettings.UserAgent, "application/json", JsonSerializer.Serialize(request), "Accept", "application/json"));

            return response;
        }
        #endregion

    }
}
