using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisOct2023.Data;
using ThesisOct2023.Repositories;
using ThesisOct2023.Helpers;
using ThesisOct2023.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace ThesisOct2023.Controllers
{
	[Authorize(Roles ="Student")]
    public class StudentController : Controller
	{
		private IFoodRepository _foodRepository;
		private IQuestionRepository _questionRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		
		public StudentController(IFoodRepository foodRepository, UserManager<ApplicationUser> userManager,IQuestionRepository questionRepository) {
			
			_foodRepository = foodRepository;
			_userManager = userManager;
			_questionRepository = questionRepository;
			

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
		[ValidateAntiForgeryToken]
		public IActionResult FoodReview(Food f)
		{

			
			Review review = new Review();
			review.FoodId = f.Id;
			review.Food = f;
			review.StudentId = _userManager.GetUserId(User);

			ViewBag.Review = review;
            string serializedReview = JsonConvert.SerializeObject(review);
            HttpContext.Session.SetString("Review", serializedReview);
            List<Question> questions = _questionRepository.GetQuestions();
			ViewBag.Questions = questions;
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult FoodInfo(Food f)
		{
			return View(f);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult SubmitReviewQuestions(IFormCollection iformCollection)
		{
            string reviewSerialized = HttpContext.Session.GetString("Review");
            Review obj = JsonConvert.DeserializeObject<Review>(reviewSerialized);
			
            //int reviewId = Convert.ToInt32(iformCollection["ReviewId"]);
			//this will change 
			string[] questionId = iformCollection["QuestionId"];

			string[] answer = iformCollection["Answer"];

			for(int q= 0;q < questionId.Count();q++)
			{
				ReviewQuestion reviewQuestion = new ReviewQuestion()
				{
					ReviewId = obj.Id,
					QuestionId = Convert.ToInt32(questionId[q]),
					Answer = Convert.ToInt32(iformCollection["Answer_"+q])
				};
				
			}
			
			
			return View("Index");
            
		}

    }
}
