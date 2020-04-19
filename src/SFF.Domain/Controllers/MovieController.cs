﻿using System;
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

            return CreatedAtAction(nameof(GetMovie), new { Title = movie.Title }, movie);
        }

        [HttpPost]
        public async Task<ActionResult> PostTrivia(Trivia trivia, Movie movie) {
            await _repository.AddTrivia(trivia, movie.Title);

            return CreatedAtAction(nameof(GetMovie), new { ID = movie.ID }, movie);
        } 

        [HttpDelete("delete")] 
        public async Task<ActionResult<Movie>> DeleteMovie(Movie movie) {
            await _repository.RemoveMovie(movie);

            return await GetMovie(movie.ID);
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
