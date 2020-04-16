using System;
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

        public async Task<int> RemoveMovie(Movie movie) {
            _context.Movies.Remove(
                _context.Movies.SingleOrDefault(s => s.ID == movie.ID)
            );

            return await _context.SaveChangesAsync();
        }

        public async Task<int> BorrowMovie(string movieTitle, int studioID) {
            var movieToBorrow = _context.Movies.FirstOrDefault(m => m.Title == movieTitle && !m.Borrowed);
            var studioThatBorrows = _context.Studios.SingleOrDefault(s => s.ID == studioID);

            if (movieToBorrow == null) 
                throw new InvalidOperationException();

            studioThatBorrows.BorrowedMovies.Add(movieToBorrow);
            movieToBorrow.Borrowed = true;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> ReturnMovie(string movieTitle, int studioID) {
            var studio = _context.Studios.SingleOrDefault(s => s.ID == studioID);
            var movieToReturn = studio.BorrowedMovies.Find(m => m.Title == movieTitle);
            if (movieToReturn == null) 
                throw new InvalidOperationException();

            movieToReturn.Borrowed = false;
            studio.BorrowedMovies.Remove(movieToReturn);

            return await _context.SaveChangesAsync();
        }
    } 
}