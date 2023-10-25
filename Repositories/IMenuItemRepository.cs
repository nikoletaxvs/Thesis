using ThesisOct2023.Models;

namespace ThesisOct2023.Repositories
{
    public interface IMenuItemRepository
    {
        public IEnumerable<MenuItem> getMenusItems(int menuId);
        public IEnumerable<MenuItem> getDaysItems(int day);
        public void addItemToMenu(MenuItem item);
        public IEnumerable<Food> getFoodByWeek(int week);
        public IEnumerable<Food> getFoodSevedNow(string timeOfDay, int week,int day);
        public IEnumerable<Food> getFoodOfDay(int day, int week);
    }
}
