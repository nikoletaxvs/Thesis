using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ThesisOct2023.Controllers
{
    [Authorize(Roles ="Cook")]
    public class CookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
