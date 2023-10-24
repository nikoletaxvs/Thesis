using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisOct2023.Data;
using ThesisOct2023.Repositories;
using ThesisOct2023.Helpers;

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
			var currentWeek = Iso8601WeekOfYear.GetIso8601WeekOfYear(DateTime.Now);
            //Get all food served this week 
            var model = _menuItemRepository.getFoodByWeek(currentWeek);
			string timeOfDay = "Closed";
			if(DateTime.Now.Hour > 8 && DateTime.Now.Hour <=12) {
                timeOfDay = "Breakfast";
			}else if(DateTime.Now.Hour > 12 && DateTime.Now.Hour <= 17)
			{
                timeOfDay = "Launch";
			}else if(DateTime.Now.Hour > 17 && DateTime.Now.Hour <= 20)
			{
                timeOfDay = "Dinner";
			}
			int day = (int)(DateTime.Now.DayOfWeek + 6) % 7; //Assuming monday is 0

            //Get food served now
            var foodServedNow = _menuItemRepository.getFoodSevedNow(timeOfDay, currentWeek,day);
			ViewBag.ServedNow = foodServedNow;
			return View(model);
		}
	}
}
