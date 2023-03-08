namespace Authentication.Services
{
    public interface ICaptchaServie
    {
        string GenerateCaptchaCode();
        string GenerateCaptchaImage(int width, int height, string captchaCode);
    }
}
