using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FApp.DAL
{
    public sealed class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions opt):base(opt)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> UserModel { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<User>();
            var users = Enumerable.Range(1, 10).Select(x =>
            {
                var u = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = x * 1 + "@t.com",
                    UserName = x + Guid.NewGuid().ToString(),
                    CreatedDateTime = DateTime.Now,
                    LastLoginDateTime = DateTime.Now
                };
                hasher.HashPassword(u, "aaa");
                return u;
            });
            base.OnModelCreating(builder);
            builder.Entity<User>().HasData(users);
        }
    }
}
