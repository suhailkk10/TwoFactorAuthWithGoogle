﻿using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
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
