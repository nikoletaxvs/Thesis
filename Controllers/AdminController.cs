﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisOct2023.Models;
using ThesisOct2023.Repositories;

namespace ThesisOct2023.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IFoodRepository _foodRepository;
        private IWebHostEnvironment _webHostEnvironment;
        private IQuestionRepository _questionRepository;
        public AdminController(IFoodRepository foodRepository, IWebHostEnvironment webHostEnvironment,IQuestionRepository questionRepository)
        {
            _foodRepository = foodRepository;
            _webHostEnvironment = webHostEnvironment;
            _questionRepository = questionRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Food() {
            return View();
        }
        //Get 
        public IActionResult PostFood()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostFood(FoodHelper obj)
        {
            if (ModelState.IsValid)
            {
                if(obj.Photo != null)
                {
                    string folder = "Food_Images";
                    folder = Guid.NewGuid().ToString() +"_"+ obj.Photo.FileName;
                    obj.ImageUrl = folder;
                    // the application will find the path thanks to this line bellow
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    //saving photo in server folder
                    await obj.Photo.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                }
                _foodRepository.AddFood(obj);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Questions()
        {
            IEnumerable<Question> questions= _questionRepository.GetQuestions();
            return View(questions);
        }
        //GET
        public IActionResult CreateQuestion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateQuestion(Question question)
        {
            _questionRepository.AddQuestion(question);
            return RedirectToAction("Index");   
        }
        //Get
        public IActionResult DeleteQuestion(int? id)
        {
            if (id != null && id !=0)
            {
                Question question =_questionRepository.GetQuestion((int)id);
                return View(question);
            }
            else
            {
                return NotFound();
            }
           
        }
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteQuestionPOST(int? id)
        {
            var question = _questionRepository.GetQuestion(id);
			if (question == null)
			{
				return NotFound();
			}

			_questionRepository.DeleteQuestion(question);
			
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");
			
        }
        public IActionResult EditQuestion(int? id)
		{
			if (id != null && id != 0)
			{
				Question question = _questionRepository.GetQuestion((int)id);
				return View(question);
			}
			else
			{
				return NotFound();
			}

		}
		[HttpPost, ActionName("Edit")]
		[ValidateAntiForgeryToken]
		public IActionResult EditQuestionPOST(Question obj)
        {
			
			if(ModelState.IsValid)
            {
				_questionRepository.UpdateQuestion(obj);
            }
            else
            {
                return NotFound();
            }

			
			
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");
			
        }
	}
}
