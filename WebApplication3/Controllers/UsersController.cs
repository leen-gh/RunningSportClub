using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Interface;
using WebApplication3.ViewModel;

namespace WebApplication3.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRunnersRepos _runnersRepos;

        public UsersController(IRunnersRepos runnersRepos) 
        { 
            _runnersRepos = runnersRepos;
        }
        
        public async Task<IActionResult> Index()
        {
            var runner = await _runnersRepos.GetAllRunners();
            List<RunnersViewModel> result = new List<RunnersViewModel>();   
            foreach(var user in runner)
            {
                var runnerVM = new RunnersViewModel()
                {
                    Id = user.Id,
                    UsreName = user.UserName,
                    Pace = user.Pace,
                    Mileage = user.Mileage
                    
                    
                };
                result.Add(runnerVM);
            }
            return View(result);
        }
        public async Task<IActionResult> Detail(string id)
        {
            var runner = await _runnersRepos.GetRunnersById(id);
            var runnerDetailVM = new RunnerDetailViewModel()
            {
                Id = runner.Id,
                UserName = runner.UserName,
                Pace = runner.Pace,
                Mileage = runner.Mileage,
                ProfileImageUrl=runner.ProfileImageUrl,
                State=runner.State,              
                City = runner.City


            };
            return View(runnerDetailVM);
        }
    }

}
