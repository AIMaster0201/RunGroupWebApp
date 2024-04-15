using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Controllers
{
    public class RaceController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IRaceRepository _raceRepository;

        public RaceController(/*ApplicationDbContext context*/IRaceRepository raceRepository)
        {
            //_context = context;
            _raceRepository = raceRepository;
        }
        public async Task<IActionResult> Index()
        {
            //List<Race> races = _context.Races.ToList();
            IEnumerable<Race> races = await _raceRepository.GetAll();
            return View(races);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            //Race race = _context.Races.Include(h => h.Address).FirstOrDefault(h => h.Id == Id);
            Race race = await _raceRepository.GetByIdAsync(Id);
            return View(race);
        }
    }
}
