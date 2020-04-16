using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFF.Domain.Models;

namespace SFF.Domain.Controllers
{
    [ApiController]
    [Route("api/Movies")]
    public class MovieController : ControllerBase {
        private MovieRepository _repository; 
        public MovieController(MovieRepository repository) {
            _repository = repository;
        }

        [HttpGet] 
        public async Task<ActionResult<Movie>> GetMovie(int id) {
            return await _repository.GetMovie(id);
        }

        [HttpGet("title")]
        public async Task<ActionResult<Movie>> GetMovie(string title) {
            return await _repository.GetMovie(title);
        }

        [HttpPost]
        public async Task<ActionResult> PostMovie(Movie movie) {
            await _repository.AddMovie(movie.Title);

            return CreatedAtAction(nameof(GetMovie), new { Title = movie.Title}, movie);
        }

        [HttpDelete("delete")] 
        public async Task<ActionResult> DeleteMovie(Movie movie) {
            await _repository.RemoveMovie(movie);

            var deleted = await GetMovie(movie.ID);
            if (deleted == null)
                return Ok();
            else 
                throw new OperationCanceledException();
        }

        [HttpPut("borrow")] 
        public async Task<ActionResult> BorrowMovie(string title, int studioID) {
            await _repository.BorrowMovie(title, studioID);

            return Ok();
        }

        [HttpPut("return")] 
        public async Task<ActionResult> ReturnMovie(string title, int studioID) {
            await _repository.ReturnMovie(title, studioID);

            return Ok();
        }
    }
}
