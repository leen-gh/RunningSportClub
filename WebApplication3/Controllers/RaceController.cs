using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Interface;
using WebApplication3.Models;
using WebApplication3.Repository;
using WebApplication3.ViewModel;

namespace WebApplication3.Controllers
{
    public class RaceController : Controller
    {
        //private readonly AppDb _context;
        private readonly IRaceRepos _raceRepos;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RaceController(IRaceRepos raceRepos, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            //_context = context;
            _raceRepos = raceRepos;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult>  Index()
        {
            /*List<Race> races = _context.Races.ToList();
            return View(races);*/
            IEnumerable<Race> races = await _raceRepos.GetAll();
            return View(races);

        }
        public async Task<IActionResult> Details(int id)
        {
            // we used include in order to get access to aadress 
            /*Race race = _context.Races.Include(a=> a.Address).FirstOrDefault(c => c.Id == id);
            return View(race);*/
            Race race = await _raceRepos.GetIdByAsync(id);
            return View(race);
        }
        public IActionResult Create()
        {
            var currentUser = _httpContextAccessor.HttpContext.User.GetUserId();
            var createRaceVM = new CreatRaceViewModel { AppUserId = currentUser };
            return View(createRaceVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatRaceViewModel raceVM )
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Photo Error");
                //_raceRepos.Add(race);
                //return RedirectToAction("Index");
            }
            else
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);
                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    RaceCategory = raceVM.RaceCategory,
                    AppUserId = raceVM.AppUserId,
                    Address = new Address
                    {
                        City = raceVM.Address.City,
                        State = raceVM.Address.State,
                        Street = raceVM.Address.Street
                    }
                };
                _raceRepos.Add(race);
                return RedirectToAction("Dashbord","Index");

            }
            return View(raceVM);
        }
    }
}

