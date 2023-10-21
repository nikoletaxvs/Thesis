using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisOct2023.Models;
using ThesisOct2023.Repositories;

namespace ThesisOct2023.Controllers
{
    [Authorize(Roles ="Cook")]
    public class CookController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        public CookController(IFoodRepository foodRepository)
        {
            _foodRepository= foodRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public  IActionResult Food()
        {
            IEnumerable<Food> food= _foodRepository.GetAllFood();
            return View(food);
        }
        public IActionResult WeeksMenu() {
            return View();
        }
        public IActionResult CreateMenu() {
            return View();
        }
    }
}
