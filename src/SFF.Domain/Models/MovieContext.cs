using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SFF.Domain.Models {
    public class MovieContext : DbContext, IMovieRepository {
        private DbSet<Movie> Movies { get; set; }

        public MovieContext(DbContextOptions<MovieContext> options) : base(options) {
            
        }

        public Movie GetMovie(int movieID)
        {
            throw new System.NotImplementedException();
        }

        public void AddMovie(string title)
        {
            throw new System.NotImplementedException();
        }

        public void AddMovie(string title, int amount)
        {
            throw new System.NotImplementedException();
        }

        public Movie GetMovie(string movieTitle)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Movie> GetMovies(string title)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveMovies(string title, int amount)
        {
            throw new System.NotImplementedException();
        }
    }
}