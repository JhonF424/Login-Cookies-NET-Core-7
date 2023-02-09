using Microsoft.AspNetCore.Mvc;
using LoginProject.Models;
using LoginProject.Resources;
using LoginProject.Services.Contract;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace LoginProject.Controllers
{
    public class StartController : Controller
    {
        private readonly IUserService _userService;
        public StartController( IUserService userService )
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register( User model )
        {
            model.Pass = Utilities.EncryptKey(model.Pass);

            User createdUser = await _userService.SaveUser(model);

            if (createdUser.UserId > 0)
                return RedirectToAction("Login", "Index");

            ViewData["Message"] = "Can't create the user";
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login( string email, string pass )
        {
            User foundUser = await _userService.GetUser(email, Utilities.EncryptKey(pass));

            if (foundUser != null)
            {
                ViewData["message"] = "Can't find the user";
                return View();
            }

            //Now we need to setting up the user auth, for that we'll create an object that can store the login info from our user 

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, foundUser.Username)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");


            return View();
        }
    }

}

