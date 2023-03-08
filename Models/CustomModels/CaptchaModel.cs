namespace Models.CustomModels
{
    public class CaptchaModel
    {
        public string ImageBase64 { get; set; }
        public string code { get; set; }
        public string TimeStamp { get; set; }
    }
}
