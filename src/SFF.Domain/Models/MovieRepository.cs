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

        public async Task<int> AddTrivia(Trivia trivia) {
            var movieFromDB = (
                from m in _context.Movies 
                where m.Title == trivia.MovieTitle
                select m
            ).FirstOrDefault();

            var triviasFromDB = (
                from t in _context.Trivias
                where t.Content == trivia.Content 
                && t.MovieTitle == trivia.MovieTitle
                select t
            ).FirstOrDefault();

            if (triviasFromDB != null)
                return 0;
            
            if (movieFromDB == null) {
                await AddMovie(trivia.MovieTitle);
                return await AddTrivia(trivia);
            }
            else {
                _context.Trivias.Add(trivia);
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<int> RemoveTrivia(Trivia trivia) {
            _context.Trivias.Remove(
                _context.Trivias.SingleOrDefault(t => t.ID == trivia.ID)
            );

            return await _context.SaveChangesAsync();
        }

        public async Task<Trivia[]> GetTrivias(string movieTitle) {
            Trivia[] trivias = await (
                from t in _context.Trivias 
                where t.MovieTitle == movieTitle
                select t
            ).ToArrayAsync();

            if (trivias.Count() == 0) 
                return new Trivia[]{new Trivia{ Content = "No trivias found for the specified movie.", MovieTitle = movieTitle }};
            else 
                return trivias;
        }

        public async Task<Movie[]> GetAllMovies() {
            return await (
                from m in _context.Movies 
                select m
            ).ToArrayAsync();
        }

        public async Task<Movie> GetMovie(int movieID) {
            return await _context.Movies.SingleOrDefaultAsync(x => x.ID == movieID);
        }
        
        public async Task<Movie> GetMovie(string movieTitle) {
            return await _context.Movies.FirstOrDefaultAsync(x => x.Title == movieTitle);
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
                throw new NullReferenceException();

            movieToBorrow.Borrowed = true;
            movieToBorrow.Borrower = studioThatBorrows;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> ReturnMovie(string movieTitle, int studioID) {
            var studio = _context.Studios.SingleOrDefault(s => s.ID == studioID);
            var movieToReturn = (
                from m in _context.Movies
                where m.Title == movieTitle
                && m.Borrowed == true 
                && m.Borrower.ID == studioID
                select m
            ).FirstOrDefault();

            if (movieToReturn == null) 
                throw new NullReferenceException();

            movieToReturn.Borrowed = false;
            movieToReturn.Borrower = null;

            return await _context.SaveChangesAsync();
        }
    } 
}