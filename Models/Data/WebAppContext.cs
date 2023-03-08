using Microsoft.EntityFrameworkCore;

namespace Models.Data
{
    public class WebAppContext : DbContext
    {
        public WebAppContext(DbContextOptions<WebAppContext> options)
           : base(options)
        {
        }

        public DbSet<UserModel> User { get; set; } = default!;
    }
}
