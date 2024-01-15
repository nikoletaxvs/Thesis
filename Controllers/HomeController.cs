using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThesisOct2023.Models;
using System.Net.Http.Headers;
using System.Data;
using Newtonsoft.Json;
using ThesisOct2023.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ThesisOct2023.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string baseURL = "https://developer.nrel.gov/api/alt-fuel-stations/v1.json?limit=1&api_key=jSMW58akzmQYtSjafRK5dFFrGn91Ng4hAkrFcrEr";

		public HomeController(ILogger<HomeController> logger, IReviewRepository reviewRepository, IFoodRepository foodRepository,UserManager<ApplicationUser> userManager)
        {
            _reviewRepository = reviewRepository;
            _foodRepository = foodRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            /*
             * Role based routing is not supported in MVC.
             * What you do is have default controller which checks for
             * the roles and redirect to that controller            
             */
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }else if (User.IsInRole("Cook"))
            {
                return RedirectToAction("Index","Cook");
            }else if (User.IsInRole("Student")){
                return RedirectToAction("Index", "Student");
            }
            ViewBag.TotalUsers = _userManager.Users.Count();
            ViewBag.TotalFood = _foodRepository.GetAllFood().Count();
            ViewBag.TotalReviews = _reviewRepository.GetReviews().Count();
            return View();
        }


        public async Task<IActionResult> Help() => View();
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}