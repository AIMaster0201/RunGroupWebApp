using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunGroupWebApp.Helpers;
using RunGroupWebApp.interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.ViewModel;
using System.Diagnostics;
using System.Globalization;

namespace RunGroupWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClubRepository _clubRepository;

        public HomeController(ILogger<HomeController> logger, IClubRepository clubRepository)
        {
            _logger = logger;
            _clubRepository = clubRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IPInfo();
            var homeviewModel = new HomeViewModel();
            try
            {
                string url = "https://ipinfo.io?token=b84d02f399e3b3";
                var info = await new HttpClient().GetStringAsync(url);
                //var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo myRI = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI.EnglishName;
                homeviewModel.City = ipInfo.City;
                homeviewModel.State = ipInfo.Region;
                if (homeviewModel.City != null)
                {
                    homeviewModel.Clubs = await _clubRepository.GetClubByCity(homeviewModel.City);
                }
                else
                {
                    homeviewModel.Clubs = null;
                }
                return View(homeviewModel);
            }
            catch (Exception ex)
            {
                homeviewModel.Clubs = null;
            }
            return View(homeviewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
