using diary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace diary.Data
{
      // database
      public class diaryContext : IdentityDbContext<User>
      {
            public diaryContext(DbContextOptions<diaryContext> options) : base(options)
            {
            }

            // 2 tables
            public DbSet<Event> Events { get; set; }
            public DbSet<Entry> Entries { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                  base.OnModelCreating(modelBuilder);

                  // because MySQL doesn't make it through 767 bytes long
                  // This bug is so annoying
                  // TODO: Make every field maxlength restrict
                  modelBuilder.Entity<User>(entity => entity.Property(m => m.Id).HasMaxLength(127));
                  modelBuilder.Entity<User>(entity => entity.Property(m => m.UserName).HasMaxLength(127));
                  modelBuilder.Entity<User>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(127));

                  modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(127));
                  modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.Name).HasMaxLength(127));
                  modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(127));

                  modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(127));
                  modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(127));

                  modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(127));
                  modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(127));

                  modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(127));
                  modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(127));
                  modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(127));

                  modelBuilder.Entity<User>().ToTable("User");
                  modelBuilder.Entity<Event>().ToTable("Event");
                  modelBuilder.Entity<Entry>().ToTable("Entry");
            }
      }
}