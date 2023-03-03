using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class DashbordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
