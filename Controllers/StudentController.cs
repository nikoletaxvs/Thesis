using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThesisOct2023.Data;
using ThesisOct2023.Repositories;

namespace ThesisOct2023.Controllers
{
	[Authorize(Roles ="Student")]
    public class StudentController : Controller
	{
		private IMenuRepository menuRepository;
		private ApplicationDbContext context;
		public StudentController(ApplicationDbContext context,IMenuRepository menuRepository) {
			this.context = context;
			this.menuRepository = menuRepository;
		}
		public IActionResult Index()
		{
			return View();
		}
	}
}
