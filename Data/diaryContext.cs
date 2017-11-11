using diary.Models;
using Microsoft.EntityFrameworkCore;

namespace diary.Data
{
      // database
      public class diaryContext : DbContext
      {
            public diaryContext(DbContextOptions<diaryContext> options) : base(options)
            {
            }

            // 2 tables
            public DbSet<Event> Events { get; set; }
            public DbSet<User> Users { get; set; }
            public DbSet<Entry> Entries { get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                  modelBuilder.Entity<User>().ToTable("User");
                  modelBuilder.Entity<Event>().ToTable("Event");
                  modelBuilder.Entity<Entry>().ToTable("Entry");
            }
      }
}