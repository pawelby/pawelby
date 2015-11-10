#region Usings
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Mvc;
    using Pawelby.Models;
    using Pawelby.ViewModels;
#endregion

namespace Pawelby.Controllers
{
    /// <summary>
    /// Authorization Controller
    /// </summary>
    public class AuthController : Controller
    {
#region Private Filds
        private SignInManager<PawelbyUser> _signInManager;
#endregion

        /// <summary>
        /// Constructor of the Auth Controller
        /// </summary>
        /// <param name="signInManager">SignIn Manager</param>
        public AuthController(SignInManager<PawelbyUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username,
                    vm.Password,
                    true, false);
                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Trips", "App");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }

            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "App");
        } 
    }
}
