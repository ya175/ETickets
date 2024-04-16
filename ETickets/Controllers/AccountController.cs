using Demo2.ViewModel;
using ETickets.Models;
using ETickets.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace ETickets.Controllers
{
    public class AccountController : Controller
    {

        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signIn;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signIn)
        {
            this.userManager = userManager;
            this.signIn = signIn;
        }
        
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(ApplicationUserVM userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser() { UserName = userVM.Name, Email = userVM.Email, Address = userVM.Address, PasswordHash = userVM.Password };

                var result = await userManager.CreateAsync(user, userVM.Password);
                await userManager.AddToRoleAsync(user, "User");

                if (result.Succeeded)
                {
                    await signIn.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }

            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userVm)
        {

            if(ModelState.IsValid)
            {

               var user =await userManager.FindByNameAsync(userVm.UserName);

                if(user != null)
                {
                    var check = await signIn.CheckPasswordSignInAsync(user,userVm.Password,true);

                    if(check!=null)
                    {

                        //cookie

                      await  signIn.SignInAsync(user, userVm.RememberMe);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("notValidP", "password is not valid");

                    }



                }
                else
                {
                    ModelState.AddModelError("notValidU", "user Name is not valid");

                }
            }

            return View();
        }




        public async Task<IActionResult> Logout()
        {
            await signIn.SignOutAsync();
            return RedirectToAction("Index", "Movie");

 
        }


    }
}
