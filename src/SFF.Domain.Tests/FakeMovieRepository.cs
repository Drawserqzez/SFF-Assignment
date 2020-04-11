using System.Collections.Generic;
using System.Linq;
using SFF.Domain.Models;

namespace SFF.Domain.Tests {
    public class FakeMovieRepository : IMovieRepository
    {  
        private List<Movie> _movies = new List<Movie>();
        public void AddMovie(string title) {
            _movies.Add(new Movie(title: title));
        }

        public void AddMovie(string title, int amount) {
            for (int i = 0; i < amount; i++) 
                _movies.Add(new Movie(i, title));
        }

        public Movie GetMovie(int movieID) {
            return _movies.FirstOrDefault(x => x.ID == movieID);
        }
        
        public Movie GetMovie(string movieTitle) {
            return _movies.FirstOrDefault(x => x.Title == movieTitle);
        }

        public IEnumerable<Movie> GetMovies(string title) {
            return _movies.FindAll(x => x.Title == title);
        }

        public void RemoveMovies(string title, int amount) {
            for (int i = amount; i < 0; i++) 
                _movies.Remove(_movies.FirstOrDefault(x => x.Title == title));
        }
    }
}