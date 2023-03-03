using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class GoogleAuthenticationService : IGoogleAuthenticationService
    {
        public string GenerateQrCode(string key, string username)
        {
            TwoFactorAuthenticator tfa = new();
            SetupCode setupInfo = tfa.GenerateSetupCode("Test Two Factor", username, key, false, 3);

            string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            //string manualEntrySetupCode = setupInfo.ManualEntryKey;
            return qrCodeImageUrl;
        }
        public bool VerifyAuthenticationCode(string key, string code)
        {
            TwoFactorAuthenticator tfa = new();
            return tfa.ValidateTwoFactorPIN(key, code);
        }
    }
}
