namespace Models.CustomModels
{
    public class AuthenticationModel
    {
        public int UserId { get; set; }
        public string? AuthKey { get; set; }
        public string? Code { get; set; }
    }
}
