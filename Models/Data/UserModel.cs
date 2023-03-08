namespace Models.Data
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAuthSet { get; set; }
        public string? AuthKey { get; set; }
    }
}
