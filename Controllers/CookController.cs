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
        private readonly IMenuItemRepository _menuItemRepository;
        public CookController(IFoodRepository foodRepository, IMenuItemRepository menuItemRepository)
        {
            _foodRepository = foodRepository;
            _menuItemRepository = menuItemRepository;
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
                int counter = 0;
                foreach(var item in model.SelectedItems)
                {
                    //Get Breakfast Item
                    MenuItem menuItem = new MenuItem();
                    menuItem.MenuId = menu.Id;
                    menuItem.FoodId =  Convert.ToInt32(item.BreakFast);
                    menuItem.Day = counter;
                    menuItem.ServedAt = "Breakfast";
                    menuItem.Menu = menu;

                        
                    _menuItemRepository.addItemToMenu(menuItem);
                        
                    //Get Launch Item
                    MenuItem menuItem2 = new MenuItem();
                    menuItem2.MenuId = menu.Id;
                    menuItem2.FoodId = Convert.ToInt32(item.Launch);
                    menuItem2.Day = counter;
                    menuItem2.ServedAt = "Launch";

                    _menuItemRepository.addItemToMenu(menuItem2);

                    //Get Dinner Item
                    MenuItem menuItem3 = new MenuItem();
                    menuItem3.MenuId = menu.Id;
                    menuItem3.FoodId = Convert.ToInt32(item.Dinner);
                    menuItem3.Day = counter;
                    menuItem3.ServedAt = "Dinner";

                    _menuItemRepository.addItemToMenu(menuItem3);

                    //Stepping into the next day
                    counter++;
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
