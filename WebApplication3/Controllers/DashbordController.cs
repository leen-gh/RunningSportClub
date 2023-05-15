using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.Eventing.Reader;
using WebApplication3.Data;
using WebApplication3.Interface;
using WebApplication3.Models;
using WebApplication3.ViewModel;

namespace WebApplication3.Controllers
{
    public class DashbordController : Controller
    {
        private readonly IUserRepos _userRepos;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPhotoService _photoService;
        public DashbordController(IUserRepos userRepos, IHttpContextAccessor httpContext,IPhotoService photoService)
        {
            _userRepos = userRepos;
            _contextAccessor = httpContext;
            _photoService = photoService;
        }
        private void MapUser(AppUser appUser, EditUserProfileVM userProfileVM, ImageUploadResult imageUpload)
        {
            appUser.Id = userProfileVM.Id;
            appUser.Pace = userProfileVM.Pace;
            appUser.Mileage = userProfileVM.Mileage;
            appUser.ProfileImageUrl = imageUpload.Url.ToString();
            appUser.City = userProfileVM.City;
            appUser.State = userProfileVM.State;
        }
        public async Task<IActionResult> Index()
        {
            var userRaces = await _userRepos.GetAllUserRaces();
            var userClubs = await _userRepos.GetAllUserClub();
            var dashbordMV = new DashbordViewModel()
            {
                Races = userRaces,
                Clubs = userClubs
            };
            return View(dashbordMV);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var currnetUser = _contextAccessor.HttpContext.User.GetUserId();
            var user = await _userRepos.GetUserById(currnetUser);
            if (user == null)
            {
                return View("error");
            }
            var editUserPro = new EditUserProfileVM()
            {
                Id = user.Id,
                Pace = user.Pace,
                Mileage = user.Mileage,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                State = user.State
            };
            return View(editUserPro);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditUserProfile(EditUserProfileVM editProfileVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "failed");
                return View("EditUserProfile", editProfileVM);
            }
            AppUser user = await _userRepos.GetUserByIdNoTracking(editProfileVM.Id);
            if (editProfileVM.Image != null)
            {
                var photo = await _photoService.AddPhotoAsync(editProfileVM.Image);

                MapUser(user,editProfileVM, photo);
                _userRepos.Update(user);
                return RedirectToAction("Index");

            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }catch (Exception ex)
                {
                    ModelState.AddModelError("", "couldnt delete photo");
                    return View(editProfileVM);
                }
                var photo = await _photoService.AddPhotoAsync(editProfileVM.Image);

                MapUser(user, editProfileVM, photo);
                _userRepos.Update(user);
                return RedirectToAction("Index");
            }
        }


    }
}
