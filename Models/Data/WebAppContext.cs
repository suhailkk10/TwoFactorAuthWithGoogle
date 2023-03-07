using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
