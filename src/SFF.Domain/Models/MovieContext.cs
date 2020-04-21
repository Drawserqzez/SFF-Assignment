using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SFF.Domain.Models {
    public class MovieContext : DbContext {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Trivia> Trivias { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public MovieContext() {
            this.Database.MigrateAsync();
        }

        // public MovieContext(DbContextOptions<MovieContext> options) : base(options) {
            
        // }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=SFF.db");
            optionsBuilder.EnableSensitiveDataLogging();
            // optionsBuilder.UseInMemoryDatabase("movies");
        }
    }
}