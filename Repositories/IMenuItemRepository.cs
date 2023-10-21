using ThesisOct2023.Models;

namespace ThesisOct2023.Repositories
{
    public interface IMenuItemRepository
    {
        public IEnumerable<MenuItem> getMenusItems(int menuId);
        public IEnumerable<MenuItem> getDaysItems(int day);
        public void addItemToMenu(MenuItem item);   
    }
}
