using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;

        public RaceRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<Race> GetByIdAsync(int Id)
        {
            return await _context.Races.Include(h => h.Address).FirstOrDefaultAsync(h => h.Id == Id);
        }

        public async Task<IEnumerable<Race>> GetRaceByCity(string city)
        {
            return await _context.Races.Where(h => h.Address.City == city).ToListAsync();
        }
        public bool Add(Race Race)
        {
            _context.Add(Race);
            return Save();
        }

        public bool Update(Race Race)
        {
            _context.Update(Race);
            return Save();
        }

        public bool Delete(Race Race)
        {
            _context.Remove(Race);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
