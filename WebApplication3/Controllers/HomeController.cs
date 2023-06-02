using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication3.Interface;
using WebApplication3.Models;
using WebApplication3.Repository;
using IPinfo;
using IPinfo.Models;
using WebApplication3.Helpers;
using WebApplication3.ViewModel;
using System.Net;
using Newtonsoft.Json;
using System.Globalization;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClubRepos _clubRepos;


        public HomeController(ILogger<HomeController> logger, IClubRepos clubRepos, IRaceRepos raceRepos)
        {
            _logger = logger;
            _clubRepos = clubRepos;
            
        }

        public  async Task<IActionResult> Index()
        {
            var ipinfo = new IpInfo() ;
            var homeVM = new HomeViewModel() ;

            try
            {
                string url = "https://ipinfo.io?token=0a8ebc122bdb7f";
                var info =new WebClient().DownloadString(url);
                ipinfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo regionInfo = new RegionInfo(ipinfo.Country) ;
                ipinfo.Country = regionInfo.EnglishName;
                homeVM.City = ipinfo.City;
                homeVM.State = ipinfo.Region;
                if(homeVM.City != null)
                {
                    homeVM.clubs = await _clubRepos.GetClubByCity(homeVM.City) ;
                }
                else
                {
                    homeVM.clubs = null;
                }
                return View(homeVM);

            }
            catch (Exception ex)
            {
                homeVM.clubs = null;
            }
            
            
            return View(homeVM);
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