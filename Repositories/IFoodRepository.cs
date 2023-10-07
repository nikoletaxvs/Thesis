using ThesisOct2023.Models;

namespace ThesisOct2023.Repositories
{
    public interface IFoodRepository
    {
        public Food GetFoodById(int id);
        public IEnumerable<Food> GetAllFood();
        

    }
}
