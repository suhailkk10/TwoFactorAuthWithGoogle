using Google.Authenticator;

namespace Authentication.Services
{
    public class GoogleAuthenticationService : IGoogleAuthenticationService
    {
        public string GenerateQrCode(string key, string username)
        {
            TwoFactorAuthenticator tfa = new();
            SetupCode setupInfo = tfa.GenerateSetupCode("Test Two Factor", username, key, false, 3);

            string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            return qrCodeImageUrl;
        }
        public bool VerifyAuthenticationCode(string key, string code)
        {
            TwoFactorAuthenticator tfa = new();
            return tfa.ValidateTwoFactorPIN(key, code);
        }
    }
}
