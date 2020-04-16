using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SFF.Domain.Models {
    public class StudioRepository {
        private MovieContext _context;

        public StudioRepository() {
            _context = new MovieContext();
        }

        public async Task<int> AddStudio(Studio studio) {
            _context.Studios.Add(studio);
            return await _context.SaveChangesAsync();
        }

        public async Task<Studio> GetStudio(int studioID) {
            return await _context.Studios.SingleOrDefaultAsync(s => s.ID == studioID);
        }

        public async Task<int> RemoveStudio(Studio studio) {
            _context.Studios.Remove(
                _context.Studios.SingleOrDefault(s => s == studio)
            );

            return await _context.SaveChangesAsync();
        }

        public async Task<int> EditStudio(Studio studio) {
            var studioToChange = _context.Studios.FirstOrDefault(s => s.ID == studio.ID);

            studioToChange.Name = studio.Name;
            studioToChange.Location = studio.Location;
            return await _context.SaveChangesAsync();
        }
    }
}