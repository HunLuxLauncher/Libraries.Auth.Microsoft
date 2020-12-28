using hu.czompisoftware.libraries.general;
using hu.hunluxlauncher.libraries.auth.microsoft.minecraft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace hu.hunluxlauncher.libraries.auth.microsoft
{
    public class MinecraftAuthentication
    {
        private AuthenticationSettings AuthSettings { get; }
        private XboxAuthentication XboxAuth { get; }
        public Authenticate Minecraft { get; }
        public GameOwnership GameOwnership { get; }
        public ProfileInfo ProfileInfo { get; }

        public MinecraftAuthentication(AuthenticationSettings authSettings, XboxAuthentication xbaut)
        {
            AuthSettings = authSettings;
            XboxAuth = xbaut;
            Minecraft = MinecraftAuthenticate(XboxAuth.XboxLive.DisplayClaims.XuiClaims[0].UserHash, XboxAuth.XboxSecurityTokenService.Token);
            GameOwnership = CheckingGameOwnership();
            try
            {
                ProfileInfo = GetProfile();
            }
            catch (UnauthorizedAccessException ex)
            {
                Logger.Error(ex.Message);
            }
        }

        #region Authenticate with Minecraft
        /// <summary>
        /// Authenticate with Minecraft
        /// </summary>
        /// <param name="user_hash">Use the 1st item's <b>UserHash</b> from array <see cref="xbox.TokenResponse.DisplayClaims.XuiClaims"/> at <see cref="XboxAuthentication.XboxLive"/>.</param>
        /// <param name="xsts_token">Use <see cref="xbox.TokenResponse.Token"/> from <see cref="XboxAuthentication.XboxSecurityTokenService"/>.</param>
        private Authenticate MinecraftAuthenticate(string user_hash, string xsts_token)
        {
            AuthenticateRequest request = new()
            {
                IdentityToken = $"XBL3.0 x={user_hash};{xsts_token}"
            };

            Authenticate response = JsonSerializer.Deserialize<Authenticate>(NetHandler.SendPostRequest(AuthLinks.McLoginLink, AuthSettings.UserAgent, "application/json", JsonSerializer.Serialize(request), "Accept", "application/json"));
            return response;
        }
        #endregion

        #region Checking game ownership
        /// <summary>
        /// Checking game ownership
        /// </summary>
        /// <param name="access_token">Use <see cref="Authenticate.AccessToken"/> from <see cref="MinecraftAuthenticate(string, string)"/>. Leave it <b>null</b> if you want to use the default account.</param>
        /// <returns>With <see cref="GameOwnership"/>.</returns>
        public GameOwnership CheckingGameOwnership(string access_token = null)
        {
            access_token ??= Minecraft.AccessToken;
            var responseStr = NetHandler.SendRequest(AuthLinks.McStoreLink, RequestMethod.GET, AuthSettings.UserAgent, null, null, "Authorization", $"Bearer {access_token}");
            GameOwnership response = JsonSerializer.Deserialize<GameOwnership>(responseStr);
            return response;
        }
        #endregion

        #region Get the profile
        /// <summary>
        /// Get the profile info
        /// </summary>
        /// <param name="access_token">Use <see cref="Authenticate.AccessToken"/> from <see cref="MinecraftAuthenticate(string, string)"/>. Leave it <b>null</b> if you want to use the default account.</param>
        /// <returns>With <see cref="ProfileInfo"/>.</returns>
        public ProfileInfo GetProfile(string access_token=null)
        {
            access_token ??= Minecraft.AccessToken;
            try
            {
                var responseStr = NetHandler.SendRequest(AuthLinks.McProfileLink, RequestMethod.GET, AuthSettings.UserAgent, null, null, "Authorization", $"Bearer {access_token}");
                ProfileInfo response = JsonSerializer.Deserialize<ProfileInfo>(responseStr);
                return response;
            }
            catch (EntryPointNotFoundException)
            {
                throw new UnauthorizedAccessException("This account does not own the game.");
            }
        }
        #endregion
    }
}
