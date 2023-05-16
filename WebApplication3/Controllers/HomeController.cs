using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication3.Interface;
using WebApplication3.Models;
using WebApplication3.Repository;

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

        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Club> clubs = await _clubRepos.GetAll();
            
            return View(clubs);
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