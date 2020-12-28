using hu.hunluxlauncher.libraries.auth.microsoft;
using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace hu.hunluxlauncher.libraries.auth.microsoft
{
    public class MicrosoftAuthentication
    {

        #region Properties
        private AuthenticationSettings AuthSettings { get; }

        public IPublicClientApplication PublicClientApp { get; }

        protected readonly string[] Scopes = new string[] { "XboxLive.signin", "XboxLive.offline_access" };
        #endregion

        public MicrosoftAuthentication(AuthenticationSettings authSettings)
        {
            AuthSettings = authSettings;
            PublicClientApp = PublicClientApplicationBuilder.Create(AuthSettings.ClientId).WithAuthority(AzureCloudInstance.AzurePublic, AuthSettings.Tenant, true).WithRedirectUri("http://localhost").Build();
        }

        #region SignIn
        /// <summary>
        /// Sign in with your Microsoft Account<br/>
        /// When calling it, a new web browser tab will open in your default browser that will prompt you to log in with your Microsoft Account. After it, if everything went well, you'll be prompted to close the tab and the process finished successfully.
        /// When that happens, this method will return with <see cref="AuthenticationResult"/>.
        /// </summary>
        /// <returns>With <see cref="AuthenticationResult"/> or will throw an <see cref="Exception"/> if a problem occurred during the authentication process.</returns>
        public async Task<AuthenticationResult> SignIn()
        {
            AuthenticationResult authResult;
            var app = PublicClientApp;

            var accounts = await app.GetAccountsAsync();
            var firstAccount = accounts.FirstOrDefault();

            try
            {
                authResult = await app.AcquireTokenSilent(Scopes, firstAccount).ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                // A MsalUiRequiredException happened on AcquireTokenSilent.
                // This indicates you need to call AcquireTokenInteractive to acquire a token
                System.Diagnostics.Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

                try
                {
                    var swvo = new SystemWebViewOptions();
                    if (AuthSettings.BrowserRedirectSuccess != null) swvo.BrowserRedirectSuccess = AuthSettings.BrowserRedirectSuccess;
                    if (AuthSettings.BrowserRedirectError != null) swvo.BrowserRedirectError = AuthSettings.BrowserRedirectError;
                    authResult = await app.AcquireTokenInteractive(Scopes).WithAccount(accounts.FirstOrDefault()).WithPrompt(Prompt.SelectAccount).WithSystemWebViewOptions(swvo).ExecuteAsync();
                }
                catch (MsalException msalex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error Acquiring Token: {System.Environment.NewLine}{msalex}");
                    throw;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error Acquiring Token Silently: {System.Environment.NewLine}{ex}");
                throw;
            }
            return authResult;
        }
        #endregion

        #region SignOut
        /// <summary>
        /// Sign out your logged in account
        /// </summary>
        /// <returns>With <b>true</b> if logged out successfully, or returns with <b>false</b> if no account was logged in or if a problem occurs during the logout process, it will throw an <see cref="MsalException"/>.</returns>
        public async Task<bool> SignOut()
        {

            var accounts = await PublicClientApp.GetAccountsAsync();

            if (accounts.Any())
            {
                try
                {
                    await PublicClientApp.RemoveAsync(accounts.FirstOrDefault());
                    return true;
                }
                catch (MsalException ex)
                {
                    Console.WriteLine($"Error signing-out user: {ex.Message}");
                    throw;
                }
            }
            return false;
        }
        #endregion

    }
}
