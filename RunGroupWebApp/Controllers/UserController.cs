using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.interfaces;
using RunGroupWebApp.ViewModel;

namespace RunGroupWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var item in users)
            {
                UserViewModel userViewModel = new UserViewModel
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Pace = item.Pace,
                    Mileage = item.Mileage,
                    ProfileImageUrl = item.ProfileImageUrl,
                };
                result.Add(userViewModel);
            }

            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userdetailViewModel = new UserDetailViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Mileage = user.Mileage
            };
            return View(userdetailViewModel);
        }
    }
}
