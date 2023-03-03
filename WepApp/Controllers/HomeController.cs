using Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using WebApp.Data;
using WebApp.Models;

namespace WepApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebAppContext _context;
        private readonly IGoogleAuthenticationService _googleAuthService;
        public HomeController(ILogger<HomeController> logger, WebAppContext context, IGoogleAuthenticationService googleAuthService)
        {
            _logger = logger;
            _context = context;
            _googleAuthService = googleAuthService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<ActionResult> IndexAsync(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _context.User.FirstOrDefaultAsync(x => x.Username == model.Username && x.Password == model.Password);
                if (result != null)
                {
                    if(!result.IsAuthSet)
                    {
                        string key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
                        string imageBase64 = _googleAuthService.GenerateQrCode(key, model.Username);
                        AuthenticationModel authentication = new()
                        {
                            AuthKey = key,
                            UserId = result.Id,
                            ImageUrl = imageBase64,
                        };
                        return RedirectToAction("SetupAuthentication", "Home",authentication);
                    }
                    else
                    {
                        AuthenticationModel authentication = new()
                        {
                            AuthKey = result.AuthKey,
                            UserId = result.Id,
                        };
                        return RedirectToAction("SetupAuthentication", "Home", authentication);
                    }
                }
            }
            return View();
        }

        public IActionResult SetupAuthentication(AuthenticationModel authentication)
        {
            ViewData["Url"] = authentication.ImageUrl;
            ViewData["AuthKey"] = authentication.AuthKey;
            ViewData["UserId"] = authentication.UserId;
            return View(authentication);
        }

        [HttpPost]
        public async Task<ActionResult> SetupAuthenticationAsync(AuthenticationModel authentication)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == authentication.UserId);
            if (user == null)
                return RedirectToAction("Index", "Home");
            bool isValid = _googleAuthService.VerifyAuthenticationCode(authentication.AuthKey, authentication.Code);
            if (isValid)
            {
                user.AuthKey = authentication.AuthKey;
                user.IsAuthSet = true;
                _context.User.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "Dashbord");
            }
            return View(authentication);
        }

        [HttpPost]
        public async Task<ActionResult> CodeValidateAsync(AuthenticationModel authentication)
        {
            var user = await _context.User.FirstOrDefaultAsync(x=> x.Id == authentication.UserId);
            if (user == null)
                return RedirectToAction("Index", "Home");
            bool isValid = _googleAuthService.VerifyAuthenticationCode(authentication.AuthKey, authentication.Code);
            if (isValid)
            {
                return RedirectToAction("Index", "Dashbord");
            }
            return View();
        }

        public IActionResult CodeValidate(AuthenticationModel authentication)
        {
            return View(authentication);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }
    }
}