using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisOct2023.Models;
using ThesisOct2023.Models.ViewModels;
using ThesisOct2023.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThesisOct2023.Helpers;
namespace ThesisOct2023.Controllers
{
    [Authorize(Roles = "Cook")]
    public class CookController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        public CookController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Food()
        {
            IEnumerable<Food> food = _foodRepository.GetAllFood();
            return View(food);
        }
        public IActionResult WeeksMenu()
        {
            return View();
        }
        public IActionResult CreateMenu()
        {
            var menuItemsData = _foodRepository.GetAllFood();
            //var model = new MenuFormView();
            //model.ItemsSelectList = new List<SelectListItem>();

            var items = new ItemSelectList();
            items.ItemsSelectList = new List<SelectListItem>();
            foreach (var item in menuItemsData)
            {
                items.ItemsSelectList.Add(new SelectListItem { Text = item.Title, Value = item.Id.ToString() });
            }
            ViewBag.MenuItems = items.ItemsSelectList;
            return View();
        }
        [HttpPost]
        public IActionResult PostMenu(MenuFormView model)
        {
            bool allDaysAreFilled = model.SelectedItems.Count == 7;
            if (allDaysAreFilled)
            {
                Menu menu = new Menu();
                menu.week = Iso8601WeekOfYear.GetIso8601WeekOfYear(DateTime.Now);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
