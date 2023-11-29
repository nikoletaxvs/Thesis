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
        private readonly IReviewQuestionRepository _reviewQuestionRepository;
        private readonly int PageSize = 8;
        public CookController(IReviewQuestionRepository reviewQuestionRepository, IFoodRepository foodRepository,IMenuItemRepository menuItemRepository, UserManager<ApplicationUser> userManager, IReviewRepository reviewRepository, IMenuRepository menuRepository)
        {
            _foodRepository = foodRepository;
            _menuItemRepository = menuItemRepository;
            _userManager = userManager;
            _reviewRepository = reviewRepository;
            _menuRepository= menuRepository;
            _reviewQuestionRepository= reviewQuestionRepository;
        }
       
        //GET /Cook
        public IActionResult Index()
        {
            ViewBag.TotalUsers = _userManager.Users.Count();
            ViewBag.TotalFood = _foodRepository.GetAllFood().Count();
            ViewBag.TotalReviews = _reviewRepository.GetReviews().Count();
            return View();
        }
        
        //GET /Cook/Food
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

        //POST /Cook/Food
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Food(string term = "",int productPage=1,string category="")
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var model = new FoodListViewModel
            {
                Foods = _foodRepository.GetAllFood()
             .Where(p=>p.Title.ToUpper().StartsWith(term.ToUpper()))
            .OrderBy(p => p.Id)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems =  _foodRepository.GetAllFood().Where(p => p.Title.ToUpper() == term.ToUpper()).Count()
                },
                CurrentCategory = ""
            };
          
            TempData["term"] = term;
            return View(model);
        }

        //GET /Cook/WeeksMenu
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
      
        //GET /Cook/CreateMenu
        public IActionResult CreateMenu()
        {
           
            //Getting food by each category. Sorted by average rating.
            var breakfast = _foodRepository.getAllByCategory("breakfast");
            var lunch = _foodRepository.getAllByCategory("lunch");
            var dinner = _foodRepository.getAllByCategory("dinner");

            //Initializing ItemSelectLists
            var breakfastitems = new ItemSelectList();
            var lunchfastitems = new ItemSelectList();
            var dinnerfastitems = new ItemSelectList();

            //Initializing Selectlists for each item type
            breakfastitems.ItemsSelectList = new List<SelectListItem>();
            lunchfastitems.ItemsSelectList = new List<SelectListItem>();
            dinnerfastitems.ItemsSelectList = new List<SelectListItem>();

            //Setting values for each time of day, ordered by their scores
            foreach (var item in breakfast)
            {
                //Adding score to the view 
                var score = item.AvgRating != null ? ((int)item.AvgRating).ToString() : "None";

                //Adding health value to the view
                var health = item.healthValue;

                //Constructing SelectList items , each contains a {Title} paired with {Rating} & {Health Value} and is assosiated with an id
                breakfastitems.ItemsSelectList.Add(new SelectListItem { Text = item.Title +" (Rating:"+score +" ,Health:"+health+ ")" , Value = item.Id.ToString() });
            }
            foreach (var item in lunch)
            {
                //Adding score to the view 
                var score = item.AvgRating != null ? ((int)item.AvgRating).ToString() : "None";

                //Adding health value to the view
                var health = item.healthValue;

                lunchfastitems.ItemsSelectList.Add(new SelectListItem { Text = item.Title +" (Rating:" + score + " ,Health:" + health + ")", Value = item.Id.ToString() });
            }
            foreach (var item in dinner)
            {
                //Adding score to the view 
                var score = item.AvgRating != null ? ((int)item.AvgRating).ToString() : "None";

                //Adding health value to the view
                var health = item.healthValue;

                dinnerfastitems.ItemsSelectList.Add(new SelectListItem { Text = item.Title +" (Rating:" + score + " ,Health:" + health + ")", Value = item.Id.ToString() });
            }

            //Storing selectlists in viewbags
            ViewBag.BreakFastItems = breakfastitems.ItemsSelectList;
            ViewBag.LunchItems = lunchfastitems.ItemsSelectList;
            ViewBag.DinnerItems = dinnerfastitems.ItemsSelectList;
            
            return View();
        }

        //POST /Cook/CreateMenu
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

        //GET /Cook/MenuHistory
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
        
        //GET /Cook/DeleteMenu
        public IActionResult DeleteMenu(int? id)
        {
            if(id != null)
            {
                _menuRepository.DeleteMenu((int)id);
            }
            

           return RedirectToAction("Index");
        }

        //POST /Cook/Enable
        [HttpPost, ActionName("Enable")]
        [ValidateAntiForgeryToken]
        public IActionResult EnablePost(Food food)
        {
           
           _foodRepository.Enable(food.Id);
            TempData["Notification"] = "Successfully enabled "+ food.Title;
            return RedirectToAction("Food");


        }

        //POST /Cook/Disable
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

        //POST /Cook/FoodInfo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FoodInfo(Food f)
        {

            //Get food object and its reviews 
            FoodReviewView model = new FoodReviewView();
            model.food = f;
            model.reviews = _reviewRepository.GetReviewsByFoodId(f.Id);
            foreach (var r in model.reviews)
            {
                model.Comments = _reviewQuestionRepository.getDistinctCommentByFoodId(f.Id);

            }
            return View(model);
        }


    }
}
