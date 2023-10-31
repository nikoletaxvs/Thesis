using ThesisOct2023.Models;

namespace ThesisOct2023.Repositories
{
    public interface IFoodRepository
    {
        public Food GetFoodById(int id);
        public IEnumerable<Food> GetAllFood();
        public void AddFood(FoodHelper food);
        public IEnumerable<Food> getFoodOfDay(int day, int week);
        public IEnumerable<Food> getFoodByWeek(int week);
        public IEnumerable<Food> getFoodSevedNow(string timeOfDay, int week, int day);
    }
}
