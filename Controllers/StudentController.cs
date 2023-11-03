using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisOct2023.Data;
using ThesisOct2023.Repositories;
using ThesisOct2023.Helpers;
using ThesisOct2023.Models;

namespace ThesisOct2023.Controllers
{
	[Authorize(Roles ="Student")]
    public class StudentController : Controller
	{
		private IFoodRepository _foodRepository;
		public StudentController(IFoodRepository _foodRepository) {
			
			this._foodRepository = _foodRepository;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Menu()
		{
            string timeOfDay = DayPoints.GetTimeOfDay();
            int currentWeek = DayPoints.GetCurrentWeekNumber();
            int day = DayPoints.GetCurrentDayNumber(); //Assuming monday is 0

            //Get all food served this week 
            var model = _foodRepository.getFoodByWeek(currentWeek);

            //Get food served now
            IEnumerable<Food> foodServedNow = _foodRepository.getFoodSevedNow(timeOfDay, currentWeek,day);

			ViewBag.ServedNow = foodServedNow;

			//Retrieving Menu Of this Week
			ViewBag.MondayFood = _foodRepository.getFoodOfDay(0, currentWeek);
			ViewBag.TuesdayFood = _foodRepository.getFoodOfDay(1, currentWeek);
			ViewBag.WednesdayFood = _foodRepository.getFoodOfDay(2, currentWeek);
			ViewBag.ThursdayFood = _foodRepository.getFoodOfDay(3, currentWeek);
			ViewBag.FridayFood = _foodRepository.getFoodOfDay(4, currentWeek);
			ViewBag.SaturdayFood = _foodRepository.getFoodOfDay(5, currentWeek);
			ViewBag.SundayFood = _foodRepository.getFoodOfDay(6, currentWeek);

			ViewBag.Role = "Student";
			return View(model);
		}
		[HttpPost]
		public IActionResult FoodReview(Food f)
		{
			Review review = new Review();
			review.FoodId = f.Id;
			review.StudentEmail = User.Identity.Name;

			return View(f);
		}
		[HttpPost]
		public IActionResult FoodInfo(Food f)
		{
			return View(f);
		}

	}
}
