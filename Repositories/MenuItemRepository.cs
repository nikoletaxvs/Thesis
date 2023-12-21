using Microsoft.EntityFrameworkCore;
using ThesisOct2023.Data;
using ThesisOct2023.Models;

namespace ThesisOct2023.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private ApplicationDbContext context;
        public MenuItemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void addItemToMenu(MenuItem item)
        {
            context.MenuItems.Add(item);
            context.SaveChanges();
        }

        public IEnumerable<MenuItem> getDaysItems(int day)
        {
            return context.MenuItems.Where(x => x.Day == day);
        }

        public IEnumerable<MenuItem> getMenusItems(int menuId)
        {
           return context.MenuItems.Where(x=>x.MenuId== menuId);
        }

        public IEnumerable<MenuItem> getWeeksItems(int week)
        {
            return context.MenuItems.Where(x=>x.Menu.week== week);
        }
    }
}
