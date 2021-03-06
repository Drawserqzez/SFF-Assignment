﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFF.Domain.Models;

namespace SFF.Domain.Controllers
{
    [ApiController]
    [Route("api/v1/Movies")]
    public class MovieController : ControllerBase {
        private MovieRepository _repository; 
        public MovieController(MovieRepository repository) {
            _repository = repository;
        }

        #region Movies
        [HttpGet] 
        public async Task<ActionResult<Movie[]>> GetMovies() {
            return await _repository.GetAllMovies();
        }

        [HttpPost]
        public async Task<ActionResult> PostMovie(Movie movie) {
            await _repository.AddMovie(movie.Title);

            return CreatedAtAction(nameof(GetMovie), new { Title = movie.Title }, movie);
        }

        [HttpGet("id/{id}")] 
        public async Task<ActionResult<Movie>> GetMovie(int id) {
            return await _repository.GetMovie(id);
        }

        [HttpGet("title")]
        public async Task<ActionResult<Movie[]>> GetMovie(string title) {
            return await _repository.GetMovies(title);
        }

        [HttpDelete("delete/movie")] 
        public async Task<ActionResult<Movie[]>> DeleteMovie(Movie movie) {
            await _repository.RemoveMovie(movie);

            return await GetMovies();
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
        #endregion Movies
        
        #region Trivia
        [HttpPost("add-trivia")]
        public async Task<ActionResult> PostTrivia(Trivia trivia) {
            await _repository.AddTrivia(trivia);

            return CreatedAtAction(nameof(GetMovie), new { Title = trivia.MovieTitle });
        } 

        [HttpGet("trivias")]
        public async Task<ActionResult<Trivia[]>> GetTrivias(string movieTitle) {
            Trivia[] result = await _repository.GetTrivias(movieTitle);
            
            return result;
        }

        [HttpDelete("delete/trivia")]
        public async Task<ActionResult<Trivia[]>> DeleteTrivia(Trivia trivia) {
            await _repository.RemoveTrivia(trivia);

            return await GetTrivias(trivia.MovieTitle);
        }
        #endregion Trivia

        #region Grades
        [HttpPost("grades")] 
        public async Task<ActionResult<Movie>> GradeMovie(Grade grade) {
            await _repository.AddGrade(grade);

            return CreatedAtAction(nameof(GetMovie), new { ID = grade.Movie.ID }, grade.Movie);
        } 

        [HttpGet("grades")]
        public async Task<ActionResult<Grade[]>> GetGrades(Movie movie) {
            return await _repository.GetGrades(movie);
        }
        #endregion Grades

        #region Label
        [HttpGet("label/{id}.{format?}")]
        [FormatFilter]
        public async Task<ActionResult<MovieLabel>> GetLabel(int id, string deliveryLocation) {
            return await _repository.GetLabelForMovie(id, deliveryLocation);
        }
        #endregion Label
    }
}