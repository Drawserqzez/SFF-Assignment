using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFF.Domain.Models;

namespace SFF.Domain.Controllers {
    [ApiController]
    [Route("api/v1/Studios")]
    public class StudioController : ControllerBase {
        private StudioRepository _repository;

        public StudioController(StudioRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<Studio[]>> GetStudios() {
            return await _repository.GetAllStudios();
        }

        [HttpPost]
        public async Task<ActionResult> PostStudio(Studio studio) {
            await _repository.AddStudio(studio);

            return CreatedAtAction(nameof(GetStudio), new { Id = studio.ID}, studio);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Studio>> GetStudio(int id) {
            return await _repository.GetStudio(id);
        }

        [HttpDelete("delete")] 
        public async Task<ActionResult<Studio>> DeleteStudio(Studio studio) {
            await _repository.RemoveStudio(studio);

            if(await _repository.GetStudio(studio.ID) == null) 
                return Ok();
            else 
                return NotFound();
        }

        [HttpGet("borrowed/{id}")] 
        public async Task<ActionResult<List<Movie>>> GetBorrowedMovies(int id) {
            return await _repository.GetBorrowedMovies(id);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<Studio>> EditStudio(Studio studio) {
            await _repository.EditStudio(studio);

            return await _repository.GetStudio(studio.ID);
        }
    }
}