namespace Authentication.Services
{
    public interface IGoogleAuthenticationService
    {
        string GenerateQrCode(string key, string username);
        bool VerifyAuthenticationCode(string key, string code);
    }
}
