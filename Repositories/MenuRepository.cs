using Humanizer;
using System.Globalization;
using ThesisOct2023.Data;
using ThesisOct2023.Models;

namespace ThesisOct2023.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private ApplicationDbContext context;

        List<Menu> menuList;
        public MenuRepository(ApplicationDbContext context)
        {
            this.context = context;
            menuList = new List<Menu>() {new Menu()
            {
                Id = 1,
                week = 1
            }
            };
        }

        public Menu GetMenu(int id)
        {
            //return context.Menus.Find(id);
            return menuList[id];
        }
        public IEnumerable<Menu> GetMenus()
        {
            return context.Menus;
        }
        public void PostMenu(Menu menu)
        {
            context.Menus.Add(menu);
        }


        public Menu GetMenuByWeek(int number)
        {
            return context.Menus.FirstOrDefault(m => m.week == number);
        }
        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public IEnumerable<int> DistinctWeeks()
        {
            return context.Menus.Select(x => x.week).Distinct().ToList();
        }

        public void DeleteMenu(int menuId) {
            //Get menu item with given Id
            Menu menu = context.Menus.Find(menuId);
            if (menu != null)
            {
                //Get its menu items and delete them one by one
                List<MenuItem> query=context.MenuItems.Where(menuItem => menuItem.MenuId == menuId).ToList();
                if(query.Any()) { 
                    foreach(var item in query)
                    {
                        context.MenuItems.Remove(item);
                    }
                }
                //Finally Delete Menu 
                context.Menus.Remove(menu);
                context.SaveChanges();
            }
        }
    }
}
