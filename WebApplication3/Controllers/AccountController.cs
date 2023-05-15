using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.ViewModel;

namespace WebApplication3.Controllers
{
    public class AccountController : Controller
    {
        //these are the manager of identity user it will give the authenticayion controllers and alot
        //and alot of proprities 
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDb _appDb;

        //then we bring the constructors and do the debindincey injection for all of it
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, AppDb appDb)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
            _appDb = appDb;
        }
        public IActionResult Login()
        {
            var response = new LoginVM();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginvm)
        {

            if(!ModelState.IsValid) return View(loginvm);
            //check if the user is vaild and then we check the password
            var user = await _userManager.FindByEmailAsync(loginvm.EmailAddress);
            
            if (user != null)
            {
                
                var password = await _userManager.CheckPasswordAsync(user, loginvm.Password);
                if (password)
                {
                    //if password is correct  login
                    var result = await _signInManager.PasswordSignInAsync(user, loginvm.Password, false,false);
                    if (result.Succeeded) 
                    { 
                        return RedirectToAction("Index", "Race"); 
                    }
                }
                //tempdata is no a good way toshow error 
                //its good for short while masseges only 
                //it would be better to use session
                //if the password is not correct
                TempData["error"] = "wrong pasword";
                return View(loginvm);
            }
            TempData["error"] = "wrong User Email";
            return View(loginvm);


        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registervm)
        {
            //modelstate when we post we check the passed data throw the model
            if (!ModelState.IsValid) return View(registervm);
            var user = await _userManager.FindByEmailAsync(registervm.EmailAddress);
            if (user != null) 
            {
                TempData["error"] = "This email is already used";
                return View(registervm);
            }
      
            var newUser = new AppUser
            {
                Email = registervm.EmailAddress,
                UserName = registervm.EmailAddress
            };
            // make sure that the password following these conditions 
            //manager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = true,
            //    RequireDigit = true,
            //    RequireLowercase = true,
            //    RequireUppercase = true,
            //};
            var newUserReg = await _userManager.CreateAsync(newUser, registervm.Password);
            if (newUserReg != null)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return RedirectToAction("Index", "Race");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Race");
        }
    }
}
 