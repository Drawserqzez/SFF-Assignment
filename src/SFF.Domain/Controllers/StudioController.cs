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
    }
}