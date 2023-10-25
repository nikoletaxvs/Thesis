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
		private IMenuItemRepository _menuItemRepository;
		public StudentController(IMenuItemRepository _menuItemRepository) {
			
			this._menuItemRepository = _menuItemRepository;
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
            var model = _menuItemRepository.getFoodByWeek(currentWeek);

            //Get food served now
            IEnumerable<Food> foodServedNow = _menuItemRepository.getFoodSevedNow(timeOfDay, currentWeek,day);

			ViewBag.ServedNow = foodServedNow;

			//Retrieving Menu Of this Week
			ViewBag.MondayFood = _menuItemRepository.getFoodOfDay(0, currentWeek);
			ViewBag.TuesdayFood = _menuItemRepository.getFoodOfDay(1, currentWeek);
			ViewBag.WednesdayFood = _menuItemRepository.getFoodOfDay(2, currentWeek);
			ViewBag.ThursdayFood = _menuItemRepository.getFoodOfDay(3, currentWeek);
			ViewBag.FridayFood = _menuItemRepository.getFoodOfDay(4, currentWeek);
			ViewBag.SaturdayFood = _menuItemRepository.getFoodOfDay(5, currentWeek);
			ViewBag.SundayFood = _menuItemRepository.getFoodOfDay(6, currentWeek);

			return View(model);
		}
	}
}
