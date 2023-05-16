using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Helpers;
using WebApplication3.Interface;
using WebApplication3.Models;
using WebApplication3.ViewModel;

namespace WebApplication3.Controllers
{
    public class ClubController : Controller
    {
        //here we are connecting to the database
        private readonly IClubRepos _clubRepos;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ClubController(IClubRepos clubRepos, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            //_context = context;
            _clubRepos = clubRepos;
            //_photoService = photoService;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
            
        }
        public async Task<IActionResult> Index()
        {
            //this code is for geting the whole data from the db table 
            // in this case we had send the data to the view
            /*List<Club> clubs = _context.Clubs.ToList();
            return View(clubs);*/
            IEnumerable<Club> clubs = await _clubRepos.GetAll();
            return View(clubs);

        }
        public async Task<IActionResult> Details(int id)
        {
            Club club = await _clubRepos.GetByIdAsync(id);
            return View(club);
        }

        //remmber that the view most had the same name 
        public IActionResult Create() 
        {
            //because the httpcontext will return huge amount of data
            //and we only need one object so we created an extention so
            //we acn easily grap this object 
            //in this way we will get the id without hitting the database
            var currentUser = _httpContextAccessor.HttpContext.User.GetUserId();
            var createClubVM = new CreateClubViewModel { AppUserId = currentUser };
            return View(createClubVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            //Whether the Form values are bound to the Model.
            //All the validations specified inside Model class using Data annotations have been passed.
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Photo Error");
            }
            else
            {
                /*_clubRepos.Add(club);
                return RedirectToAction("Index");*/
                var result = await _photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    ClubCategory = clubVM.ClubCategory,
                    AppUserId = clubVM.AppUserId,
                    Address = new Address
                    {
                        City = clubVM.Address.City,
                        State = clubVM.Address.State,
                        Street = clubVM.Address.Street
                    }
                };
                _clubRepos.Add(club);
                return RedirectToAction("Dashbord", "Index");



            }
            return View(clubVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepos.GetByIdAsync(id);
            if (club == null)return View("Error");

            var clubvm = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                Image = club.Image,
                ClubCategory = club.ClubCategory

            };
            return View(clubvm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "failed to edit");
                return RedirectToAction("Edit", clubVM);
            }
          
                var userId = await _clubRepos.GetByIdAsyncNoTracking(id);
                if (userId != null)
                {
                    try
                    {
                        await _photoService.DeletePhotoAsync(userId.Image);
                    } catch (Exception ex)
                    {
                        ModelState.AddModelError("", "could not delet");
                        return View(clubVM);
                    }
                
                    var result = await _photoService.AddPhotoAsync(clubVM.Image);
                    var club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    ClubCategory = clubVM.ClubCategory,
                    Address = new Address
                    {
                        City = clubVM.Address.City,
                        State = clubVM.Address.State,
                        Street = clubVM.Address.Street
                    }
                };
                _clubRepos.Update(club);
                return RedirectToAction("index");
            }

            else
             {
               return View(clubVM);
             }

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _clubRepos.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await _clubRepos.GetByIdAsync(id);

            if (clubDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(clubDetails.Image))
            {
                _ = _photoService.DeletePhotoAsync(clubDetails.Image);
            }

            _clubRepos.Delete(clubDetails);
            return RedirectToAction("Index");
        }

    }
}

