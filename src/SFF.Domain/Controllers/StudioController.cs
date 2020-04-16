using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFF.Domain.Models;

namespace SFF.Domain.Controllers {
    [ApiController]
    [Route("api/Studios")]
    public class StudioController : ControllerBase {
        private StudioRepository _repository;

        public StudioController(StudioRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<Studio>> GetStudio(int id) {
            return await _repository.GetStudio(id);
        }

        [HttpPost]
        public async Task<ActionResult> PostStudio(Studio studio) {
            await _repository.AddStudio(studio);

            return CreatedAtAction(nameof(GetStudio), new { Id = studio.ID}, studio);
        }

        [HttpDelete("delete")] 
        public async Task<ActionResult> DeleteStudio(Studio studio) {
            await _repository.RemoveStudio(studio);

            var deleted = await GetStudio(studio.ID);
            
            if (deleted == null)
                return Ok();
            else 
                throw new OperationCanceledException();
        }

        [HttpGet("borrowed")] 
        public async Task<ActionResult<List<Movie>>> GetBorrowedMovies(int studioID) {
            return await _repository.GetBorrowedMovies(studioID);
        }

        [HttpPut("edit")]
        public async Task<ActionResult> EditStudio(Studio studio) {
            await _repository.EditStudio(studio);

            var changedStudio = await _repository.GetStudio(studio.ID); 
            if (changedStudio.Location == studio.Location && changedStudio.Name == studio.Name) 
                return Ok();
            else 
                throw new ArgumentException();
        }
    }
}