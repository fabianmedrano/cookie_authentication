using cookie_authentication.Data;
using cookie_authentication.Views.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using cookie_authentication.Models;
using System.Text;
using System.Security.Cryptography;
using cookie_authentication.Services;

namespace cookie_authentication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly AuthService _authenticationService;
        public AccountController(ApplicationDbContext context, AuthService authenticationService) {
            _context = context;
            _authenticationService = authenticationService;
        }




        [HttpGet]
        public IActionResult Login() {
            UserLoginViewMosdel userViewModels = new UserLoginViewMosdel();
            return View(userViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewMosdel user)
        {

            var userDb = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == u.Password);
            var userRoles = userDb?.UserRoles.Select(ur => ur.Role.Name).ToList();
            if (userDb != null) {

                var claims = new List<Claim> {
                 
                    new Claim (ClaimTypes.Email, userDb.Email),
                    new Claim (ClaimTypes.Name, userDb.Name),
                //  new Claim("LastChanged", { /*Database Value*/} ),
                };

                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }


                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme
                 );

                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties
                 );
            }
            else {
                return View();
            }


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel user)
        {

            var userDB = new User
            {
                Name = user.Name,
                Email = user.Email,
                Username = user.Username,
                Password = user.Password
            };
            // Asignar roles al usuario
            userDB.UserRoles = new List<UserRole>
            {
                new UserRole { RoleId =1 }
            };
            // Save the user to the database
            _context.Users.Add(userDB);
            await _context.SaveChangesAsync();

            // Sign in the user
            await _authenticationService.SignInUserAsync(userDB);

            return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public  JsonResult CheckUsername(string username)
        {
            var exists =  _context.Users.Any(u => u.Username == username);

            return Json(!exists);
        }

    }
   



}

