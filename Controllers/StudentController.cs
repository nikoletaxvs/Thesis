﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisOct2023.Data;
using ThesisOct2023.Repositories;
using ThesisOct2023.Helpers;
using ThesisOct2023.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Drawing.Printing;
using ThesisOct2023.Models.ViewModels;

namespace ThesisOct2023.Controllers
{
	[Authorize(Roles ="Student")]
    public class StudentController : Controller
	{
		private IFoodRepository _foodRepository;
		private IQuestionRepository _questionRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private IReviewRepository _reviewRepository;
		private IReviewQuestionRepository _reviewQuestionRepository;
        private readonly int PageSize = 8;

        public StudentController(IFoodRepository foodRepository, UserManager<ApplicationUser> userManager,IQuestionRepository questionRepository,IReviewQuestionRepository reviewQuestionRepository, IReviewRepository reviewRepository) {
			
			_foodRepository = foodRepository;
			_userManager = userManager;
			_questionRepository = questionRepository;
			_reviewQuestionRepository = reviewQuestionRepository;
			_reviewRepository = reviewRepository;

		}
		
		public IActionResult Index()
		{
            ViewBag.TotalUsers = _userManager.Users.Count();
            ViewBag.TotalFood = _foodRepository.GetAllFood().Count();
            ViewBag.TotalReviews = _reviewRepository.GetReviews().Count();
			
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

			ViewBag.Rated = _reviewRepository.GetStudentReviewFoodId(_userManager.GetUserId(User));
			return View(model);
		}
        //Get
        public ViewResult Food(string? category, int productPage = 1)
		{
            ViewBag.Rated = _reviewRepository.GetStudentReviewFoodId(_userManager.GetUserId(User));

            return View(new FoodListViewModel
            {
                Foods = _foodRepository.getAllEnabledFood()
             .Where(p => category == null || p.Category.ToUpper() == category.ToUpper())
            .OrderBy(p => p.Id)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null
            ? _foodRepository.getAllEnabledFood().Count()
            : _foodRepository.getAllEnabledFood().Where(e =>
            e.Category.ToUpper() == category.ToUpper()).Count()
                },
                CurrentCategory = category
            });
        }

        
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult FoodReview(Food f)
		{

			
			Review review = new Review();
			review.FoodId = f.Id;
			
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
            Review review = JsonConvert.DeserializeObject<Review>(reviewSerialized);
			if (review != null)
			{
                try
                {
                    _reviewRepository.AddReview(review);
                   
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View("Index");
                }
               
            }
			
            //int reviewId = Convert.ToInt32(iformCollection["ReviewId"]);
			//this will change 
			string[] questionId = iformCollection["QuestionId"];

			string[] answer = iformCollection["Answer"];

			for(int q= 0;q < questionId.Count();q++)
			{
				ReviewQuestion reviewQuestion = new ReviewQuestion()
				{
					ReviewId = review.Id,
					QuestionId = Convert.ToInt32(questionId[q]),
					Answer = Convert.ToInt32(iformCollection["Answer_"+q])
				};
				if(reviewQuestion != null)
				{
					try
					{
                        _reviewQuestionRepository.AddReviewQuestion(reviewQuestion);
						var food = _foodRepository.GetFoodById(review.FoodId);
						float sum = 0f;
						for(int q2 = 0; q2 < questionId.Count(); q2++)
						{
							sum += Convert.ToInt32(iformCollection["Answer_" + q2]);
                        }

						sum = sum / questionId.Count();
						_foodRepository.updateFoodRating(food, sum);
						
                    }catch(ValidationException ex)
					{
						ModelState.AddModelError(string.Empty, ex.Message);
					}
					
				}
				
			}
			
			
			return View("Index");
            
		}

    }
}
