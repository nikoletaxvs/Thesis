using Microsoft.AspNetCore.Mvc;
using ThesisOct2023.Data;
using ThesisOct2023.Repositories;

namespace ThesisOct2023.Components
{
    public class NavigationCategoriesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _ctx;
        public NavigationCategoriesViewComponent(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        /*
         The view component’s Invoke method is called when the component is used
        in a Razor view, and the result of the Invoke method is inserted into the
        HTML sent to the browser
         */

        public IViewComponentResult Invoke()
        {
            return View(_ctx.Foods
                .Select(f => f.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
