using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisOct2023.Models;
using ThesisOct2023.Models.ViewModels;
using ThesisOct2023.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThesisOct2023.Helpers;
using NuGet.Protocol.Core.Types;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Identity;

namespace ThesisOct2023.Controllers
{
    [Authorize(Roles = "Cook")]
    public class CookController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReviewRepository _reviewRepository;
        private readonly int PageSize = 8;
        public CookController(IFoodRepository foodRepository,IMenuItemRepository menuItemRepository, UserManager<ApplicationUser> userManager, IReviewRepository reviewRepository, IMenuRepository menuRepository)
        {
            _foodRepository = foodRepository;
            _menuItemRepository = menuItemRepository;
            _userManager = userManager;
            _reviewRepository = reviewRepository;
            _menuRepository= menuRepository;
        }
        public IActionResult Index()
        {
            ViewBag.TotalUsers = _userManager.Users.Count();
            ViewBag.TotalFood = _foodRepository.GetAllFood().Count();
            ViewBag.TotalReviews = _reviewRepository.GetReviews().Count();
            return View();
        }
        public ViewResult Food(string? category, int productPage=1)

        => View(new FoodListViewModel {
             Foods = _foodRepository.GetAllFood()
             .Where(p=>category ==null || p.Category.ToUpper() == category.ToUpper())
            .OrderBy(p => p.Id)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize),
            PagingInfo = new PagingInfo {
            CurrentPage = productPage,
            ItemsPerPage = PageSize,
            TotalItems = category == null
            ? _foodRepository.GetAllFood().Count()
            :  _foodRepository.GetAllFood().Where(e =>
            e.Category.ToUpper() == category.ToUpper()).Count()
            },
            CurrentCategory = category
            });
        public IActionResult WeeksMenu()
            {

            int currentWeek = DayPoints.GetCurrentWeekNumber();
            //Get all food served this week 
            //var model = _foodRepository.getFoodByWeek(currentWeek);
            var model = _menuItemRepository.getWeeksItems(currentWeek);
            //Retrieving Menu Of this Week
            ViewBag.MondayFood = _foodRepository.getFoodOfDay(0, currentWeek);
            ViewBag.TuesdayFood = _foodRepository.getFoodOfDay(1, currentWeek);
            ViewBag.WednesdayFood = _foodRepository.getFoodOfDay(2, currentWeek);
            ViewBag.ThursdayFood = _foodRepository.getFoodOfDay(3, currentWeek);
            ViewBag.FridayFood = _foodRepository.getFoodOfDay(4, currentWeek);
            ViewBag.SaturdayFood = _foodRepository.getFoodOfDay(5, currentWeek);
            ViewBag.SundayFood = _foodRepository.getFoodOfDay(6, currentWeek);
            return View(model);
        }
        public IActionResult CreateMenu()
        {
           
            //Getting food by each category. Sorted by average rating.
            var breakfast = _foodRepository.getAllByCategory("breakfast");
            var lunch = _foodRepository.getAllByCategory("lunch");
            var dinner = _foodRepository.getAllByCategory("dinner");

            var breakfastitems = new ItemSelectList();
            var lunchfastitems = new ItemSelectList();
            var dinnerfastitems = new ItemSelectList();

            breakfastitems.ItemsSelectList = new List<SelectListItem>();
            lunchfastitems.ItemsSelectList = new List<SelectListItem>();
            dinnerfastitems.ItemsSelectList = new List<SelectListItem>();

            foreach (var item in breakfast)
            {
                var score = item.AvgRating != null ? ((int)item.AvgRating).ToString() : "None";
                breakfastitems.ItemsSelectList.Add(new SelectListItem { Text = item.Title +" (Rating:"+score +")" , Value = item.Id.ToString() });
            }
            foreach (var item in lunch)
            {
                var score = item.AvgRating != null ? ((int)item.AvgRating).ToString() : "None";
                lunchfastitems.ItemsSelectList.Add(new SelectListItem { Text = item.Title + " (Rating:" + score + ")", Value = item.Id.ToString() });
            }
            foreach (var item in dinner)
            {
                var score = item.AvgRating != null ? ((int)item.AvgRating).ToString() : "None";
                dinnerfastitems.ItemsSelectList.Add(new SelectListItem { Text = item.Title + " (Rating:" + score + ")", Value = item.Id.ToString() });
            }

            ViewBag.BreakFastItems = breakfastitems.ItemsSelectList;
            ViewBag.LunchItems = lunchfastitems.ItemsSelectList;
            ViewBag.DinnerItems = dinnerfastitems.ItemsSelectList;
            
            return View();
        }
        [HttpPost]
        public IActionResult CreateMenu(MenuFormView model)
        {
            //check if menu is already submitted
            if (_menuRepository.GetMenuByWeek(Iso8601WeekOfYear.GetIso8601WeekOfYear(DateTime.Now)) == null)
            {

                bool allDaysAreFilled = model.SelectedItems.Count == 7;
                if (allDaysAreFilled)
                {
                    Menu menu = new Menu();
                    menu.week = Iso8601WeekOfYear.GetIso8601WeekOfYear(DateTime.Now);
                    int counter = 0;
                    foreach (var item in model.SelectedItems)
                    {
                        //Get Breakfast Item
                        MenuItem menuItem = new MenuItem();
                        menuItem.MenuId = menu.Id;
                        menuItem.FoodId = Convert.ToInt32(item.BreakFast);
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
                    //return RedirectToAction("Index");
                }
                TempData["Notification"] = "Successfully created this week's menu";
                return RedirectToAction("Index");
               
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        public IActionResult MenuHistory(int productPage = 1)
        {
            IEnumerable<int> distinctWeeks = _menuRepository.DistinctWeeks();
            List<MenuViewModel> menus = new List<MenuViewModel>();
            foreach(var w in distinctWeeks)
            {
                menus.Add(new MenuViewModel()
                {
                    weekId=w,
                    Monday = (List<Food>)_foodRepository.getFoodOfDay(0, w),
                    Tuesday = (List<Food>)_foodRepository.getFoodOfDay(1, w),
                    Wednesday = (List<Food>)_foodRepository.getFoodOfDay(2, w),
                    Thursday = (List<Food>)_foodRepository.getFoodOfDay(3, w),
                    Friday = (List<Food>)_foodRepository.getFoodOfDay(4, w),
                    Saturady = (List<Food>)_foodRepository.getFoodOfDay(5, w),
                    Sunday = (List<Food>)_foodRepository.getFoodOfDay(6, w),
                    PagingInfo=new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemsPerPage = PageSize,
                        TotalItems = distinctWeeks.Count()
                    }
                    });
            }
           
            return View(menus);
           
        }
        public IActionResult DeleteMenu(int? id)
        {
            if(id != null)
            {
                _menuRepository.DeleteMenu((int)id);
            }
            

           return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Enable")]
        [ValidateAntiForgeryToken]
        public IActionResult EnablePost(Food food)
        {
           
           _foodRepository.Enable(food.Id);
            TempData["Notification"] = "Successfully enabled "+ food.Title;
            return RedirectToAction("Food");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Disable(Food food)
        {
            try
            {
                _foodRepository.Disable(food.Id);
                TempData["Notification"] = "Successfully disabled " + food.Title;
            }
            catch (Exception ex)
            {
                throw;
            }
            return RedirectToAction("Food");
        }

    }
}
