using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SFF.Domain.Models;

namespace SFF.Domain.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase {
        private IMovieRepository _repository; 
        public MovieController(IMovieRepository repository) {
            _repository = repository;
        }

        [HttpGet] 
        public Movie GetMovie(int id) {
            return _repository.GetMovie(id);
        }

        [HttpGet]
        public Movie GetMovie(string title) {
            return _repository.GetMovie(title);
        }

        [HttpGet]
        public IEnumerable<Movie> GetMovies(string title) {
            return _repository.GetMovies(title);
        }

        [HttpPost]
        public void PostMovie(string title, int amount) {
            _repository.AddMovie(title, amount);
        }

        [HttpPut]
        public void AdjustAmountOfMovies(string title, int changeInNumbers) {
            if (changeInNumbers == 0) 
                return; 
            else if (changeInNumbers > 0) 
                _repository.AddMovie(title, changeInNumbers);
            else if (changeInNumbers < 0)
                _repository.RemoveMovies(title, changeInNumbers);
        }
    }
}
