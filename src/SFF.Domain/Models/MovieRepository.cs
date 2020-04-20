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
        
        public async Task<Movie[]> GetMovies(string movieTitle) {
            return await _context.Movies.Where(x => x.Title == movieTitle).ToArrayAsync();
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

        public async Task<Grade[]> GetGrades(Movie movie) {
            var grades = await (
                from g in _context.Grades 
                where g.Movie.Title == movie.Title
                select g
            ).ToArrayAsync();

            return grades;
        }

        public async Task<int> AddGrade(Grade grade) {
            if ((int)grade.Rating > 5)
                grade.Rating = StarAmount.Five;
            else if ((int)grade.Rating < 1) 
                grade.Rating = StarAmount.One;
            
            var previousGrade = (
                from g in _context.Grades 
                where g.Movie.ID == grade.Movie.ID
                && g.Studio.ID == grade.Studio.ID
                select g
            ).FirstOrDefault();

            var studio = _context.Studios.FirstOrDefault(s => s.ID == grade.Studio.ID);
            var movie = _context.Movies.FirstOrDefault(m => m.ID == grade.Movie.ID);
            grade.Movie = (movie != null) ? movie : grade.Movie;
            grade.Studio = (studio != null) ? studio : grade.Studio;

            if (previousGrade != null)
                previousGrade.Rating = grade.Rating;
            else
                _context.Grades.Add(grade);
            

            return await _context.SaveChangesAsync();
        }
    } 
}