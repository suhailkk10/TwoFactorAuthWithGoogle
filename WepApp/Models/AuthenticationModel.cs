namespace WebApp.Models
{
    public class AuthenticationModel
    {
        public int UserId { get; set; }
        public string AuthKey { get; set; }
        public string ImageUrl { get; set; }
        public string Code { get; set; }
    }
}
