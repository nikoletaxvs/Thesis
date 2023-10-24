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
        public IEnumerable<Food> getFoodByWeek(int week)
        {
            var query = from f in context.Foods join m_item in context.MenuItems on f.Id equals m_item.FoodId 
                        join m in context.Menus on m_item.MenuId equals m.Id 
                        where m.week == week
                        select f;
            return query.ToList();
        }
        public IEnumerable<Food> getFoodSevedNow(string timeOfDay,int week)
        {
            var query = from f in context.Foods
                        join m_item in context.MenuItems on f.Id equals m_item.FoodId
                        where m_item.ServedAt.ToUpper() == timeOfDay.ToUpper()
                        join m in context.Menus on m_item.MenuId equals m.Id
                        where m.week == week
                        select f;
            return query.ToList();
        }
    }
}
