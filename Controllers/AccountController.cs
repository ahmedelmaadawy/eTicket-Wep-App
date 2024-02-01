using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
        public IActionResult Login() => View(new LoginVM());
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginvm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginvm);
            }
            var user = await _userManager.FindByEmailAsync(loginvm.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginvm.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginvm.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                TempData["Error"] = "Wrong Email Or Password Please Try Again";
                return View(loginvm);
            }
            TempData["Error"] = "Wrong Email Or Password Please Try Again";
            return View(loginvm);
        }
        public IActionResult Register() => View("Register", new RegisterVm());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }
            var user = await _userManager.FindByEmailAsync(registerVm.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This Email Address is already in use";
                return View(registerVm);
            }
            var newUser = new ApplicationUser()
            {
                FullName = registerVm.FullName,
                Email = registerVm.EmailAddress,
                UserName = registerVm.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVm.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return View("RegisterCompleted");
            }
            TempData["Error"] = "Password Must Contain Numbers And Upper And Lower Case Characters And Special Characters";
            return View(registerVm);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
    }
}
