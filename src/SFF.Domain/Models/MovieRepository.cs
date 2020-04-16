using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SFF.Domain.Models {
    public class MovieRepository {
        private MovieContext _context;

        public MovieRepository() {
            _context = new MovieContext();
        }

        public async Task<int> AddMovie(string title) {
            _context.Movies.Add(new Movie{ Title = title });
            return await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovie(int movieID) {
            return await _context.Movies.SingleOrDefaultAsync(x => x.ID == movieID);
        }
        
        public async Task<Movie> GetMovie(string movieTitle) {
            return await _context.Movies.SingleOrDefaultAsync(x => x.Title == movieTitle);
        }
    }
}