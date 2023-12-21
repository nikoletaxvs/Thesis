using ThesisOct2023.Models;
using ThesisOct2023.Models.ViewModels;

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
        public void updateFoodRating(Food food, float rating);
        public IEnumerable<Food> getAllByCategory(string category);
        public void Enable(int foodId);
        public void Disable(int foodId);
        public List<Food> getAllEnabledFood();
        public List<FoodChartViewModel> GetFoodCharts();
        public List<FoodChartViewModel> GetFoodChartsContainingTerm(string term);
    }
}
