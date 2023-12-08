using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using ThesisOct2023.Models;
using ThesisOct2023.Models.ViewModels;
using ThesisOct2023.Repositories;

namespace ThesisOct2023.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IFoodRepository _foodRepository;
        private IWebHostEnvironment _webHostEnvironment;
        private IQuestionRepository _questionRepository;
        private IReviewRepository _reviewRepository;
        private UserManager<ApplicationUser> _userManager;
        public int PageSize = 6;
        public AdminController(IReviewRepository reviewRepository, IFoodRepository foodRepository, IWebHostEnvironment webHostEnvironment,IQuestionRepository questionRepository, UserManager<ApplicationUser> userManager)
        {
            _foodRepository = foodRepository;
            _webHostEnvironment = webHostEnvironment;
            _questionRepository = questionRepository;
            _userManager = userManager;
            _reviewRepository = reviewRepository;
        }

        //GET /Admin
        public IActionResult Index(int productPage = 1)
        {
            //First Diagram - Total users of each role
            ViewBag.StudentsCounter= _userManager.Users.Where(x=> x.UserRole.ToUpper() =="STUDENT").Count(); 
            ViewBag.AdminsCounter= _userManager.Users.Where(x=> x.UserRole.ToUpper() =="ADMIN").Count(); 
            ViewBag.CooksCounter= _userManager.Users.Where(x=> x.UserRole.ToUpper() =="COOK").Count();

            //Second Diagram - Registered Users over time
            ViewBag.UsersOverTime =_userManager.Users.GroupBy(x => x.RegistrationDate.Year)
                .Select(group => new { 
                    RegisterDateName = group.Key,
                    HeadCount = group.Count(),
                })
                .OrderBy(dc=>dc.RegisterDateName)
                .ToList();
            List<string> dates = new List<string>();
            List<int> headcounts = new List<int>();

            foreach (var d in ViewBag.UsersOverTime)
            {
                string formattedDate = d.RegisterDateName.ToString();
                dates.Add(formattedDate);
            }
            foreach (var h in ViewBag.UsersOverTime)
            {
                headcounts.Add(h.HeadCount);
            }
            ViewBag.dates = dates;
            ViewBag.headcounts = headcounts;

            ViewBag.TotalUsers = _userManager.Users.Count();
            ViewBag.TotalFood = _foodRepository.GetAllFood().Count();
            ViewBag.TotalReviews = _reviewRepository.GetReviews().Count();

            IEnumerable<FoodChartViewModel> model = _foodRepository.GetFoodCharts();

            var m = new ChartsView
            {
                charts = _foodRepository.GetFoodCharts()
                                        .OrderBy(p => p.Id)
                                        .Skip((productPage - 1) * PageSize)
                                        .Take(PageSize),
                                        PagingInfo = new PagingInfo
                                        {
                                            CurrentPage = productPage,
                                            ItemsPerPage = PageSize,
                                            TotalItems = _foodRepository.GetFoodCharts().Count()
                                        }

            };
            return View(m);
        }
       
        //POST /Admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string term="")
        {
            term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
            var foodData = _foodRepository.GetFoodChartsContainingTerm(term);
            var charts = new ChartsView { charts = foodData , PagingInfo = new PagingInfo
            {
                CurrentPage = 1,
                ItemsPerPage = PageSize,
                TotalItems = foodData.Count()
            }
            };
            TempData["term"] = term;
            return View(charts);
        }
       
        ////GET Admin/Food
        //public IActionResult Food() {
        //    return View();
        //}

        //GET Admin/PostFood
        public IActionResult PostFood()
        {
            return View();
        }

        //POST Admin/PostFood
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
                TempData["Notification"] = "Successfully Added new Food";
                return RedirectToAction("Index");
            }
            TempData["Notification"] = "There was a problem";
            return View();
        }

        //GET Admin/Questions
        public IActionResult Questions()
        {
            IEnumerable<Question> questions= _questionRepository.GetQuestions();
            return View(questions);
        }
       
        //GET Admin/CreateQuestion
        public IActionResult CreateQuestion()
        {
            return View();
        }

        //POST Admin/CreateQuestion
        [HttpPost]
        public IActionResult CreateQuestion(Question question)
        {
            _questionRepository.AddQuestion(question);
            TempData["Notification"] = "Successfully Added A New Question";
            return RedirectToAction("Index");   
        }

        //GET Admin/DeleteQuestion
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

        //DELETE Admin/Delete
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

            TempData["Notification"] = "Successfully deleted a question about " + question.Title;
            return RedirectToAction("Index");
			
        }

        //GET Admin/EditQuestion
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

        //POST Admin/Edit
		[HttpPost, ActionName("Edit")]
		[ValidateAntiForgeryToken]
		public IActionResult EditQuestionPOST(Question obj)
        {
			
			if(ModelState.IsValid)
            {
				_questionRepository.UpdateQuestion(obj);
                TempData["Notification"] = "Successfully edited a question about " + obj.Title;
            }
            else
            {
                return NotFound();
            }

			
			
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");
			
        }

        //public IActionResult FoodCharts(string term="")
        //{
        //    term = string.IsNullOrEmpty(term) ? "" : term.ToLower();
        //    var foodData = new FoodChartViewModel();
        //    var charts = _foodRepository.GetFoodChartsContainingTerm(term);
        //    return View(charts);
        //}

    }
}
