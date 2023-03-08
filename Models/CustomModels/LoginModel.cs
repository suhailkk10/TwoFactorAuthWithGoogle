namespace Models.CustomModels
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string CaptchaCode { get; set; }
        public string SystemCaptcha { get; set; }
        public string TimeStamp { get; set; }
    }
}
