using Microsoft.AspNetCore.Authorization;
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
        public AdminController(IFoodRepository foodRepository, IWebHostEnvironment webHostEnvironment)
        {
            _foodRepository = foodRepository;
            _webHostEnvironment = webHostEnvironment;
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
    }
}
