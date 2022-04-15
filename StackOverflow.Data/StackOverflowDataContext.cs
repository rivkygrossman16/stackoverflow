using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Data
{
    class StackOverflowDataContext : DbContext
    {
        private string _connectionString;

        public StackOverflowDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionTags> QuestionTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<QuestionTags>()
               .HasKey(qt => new { qt.QuestionId, qt.TagId });


            modelBuilder.Entity<Like>()
                .HasKey(qt => new { qt.QuestionId, qt.UserId });
            base.OnModelCreating(modelBuilder);
        }

    }
}
