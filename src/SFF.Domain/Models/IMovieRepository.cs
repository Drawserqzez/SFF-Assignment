using System.Collections.Generic;

namespace SFF.Domain.Models {
    public interface IMovieRepository {
        void AddMovie(string title);
        void AddMovie(string title, int amount);
        Movie GetMovie(int movieID);
        Movie GetMovie(string movieTitle);
        IEnumerable<Movie> GetMovies(string title);
        void RemoveMovies(string title, int amount);
    }
}