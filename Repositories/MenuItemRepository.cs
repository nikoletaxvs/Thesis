using Microsoft.EntityFrameworkCore;
using ThesisOct2023.Data;
using ThesisOct2023.Models;

namespace ThesisOct2023.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private ApplicationDbContext context;
        public MenuItemRepository(ApplicationDbContext ctx)
        {
            context= ctx;
        }
        public void addItemToMenu(MenuItem item, int menuId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuItem> getDaysItems(int day)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuItem> getMenusItems(int menuId)
        {
            throw new NotImplementedException();
        }
    }
}
