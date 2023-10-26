using Microsoft.AspNetCore.Mvc;
using ThesisOct2023.Repositories;

namespace ThesisOct2023.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IFoodRepository _foodRepository;
        public NavigationMenuViewComponent(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        /*
         The view component’s Invoke method is called when the component is used
        in a Razor view, and the result of the Invoke method is inserted into the
        HTML sent to the browser
         */

        public IViewComponentResult Invoke()
        {
            return View(_foodRepository.GetAllFood()
                .Select(f => f.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
