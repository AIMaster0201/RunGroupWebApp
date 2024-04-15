using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Controllers
{
    public class ClubController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IClubRepository _clubRepository;

        public ClubController(/*ApplicationDbContext context*/IClubRepository clubRepository)
        {
            //_context = context;
            _clubRepository = clubRepository;
        }
        public async Task<IActionResult> Index()
        {
            //List<Club> clubs = _context.Clubs.ToList();
            IEnumerable<Club> clubs = await _clubRepository.GetAll();
            return View(clubs);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            //Club club = _context.Clubs.Include(h => h.Address).FirstOrDefault(h => h.Id == Id);
            Club club = await _clubRepository.GetByIdAsync(Id);
            return View(club);
        }

    }
}
