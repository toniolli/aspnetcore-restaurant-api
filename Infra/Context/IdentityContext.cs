using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infra.Identity;
namespace Infra.Context
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.UserName).HasMaxLength(100);
                entity.Property(u => u.NormalizedUserName).HasMaxLength(100);
                entity.Property(u => u.Email).HasMaxLength(150);
                entity.Property(u => u.NormalizedEmail).HasMaxLength(150);


            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.Property(r => r.Name).HasMaxLength(100);
                entity.Property(r => r.NormalizedName).HasMaxLength(100);
            });
        }
    }

}
