using Microsoft.AspNetCore.Mvc;

namespace ThesisOct2023.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
