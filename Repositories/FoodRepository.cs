using System.Drawing;
using ThesisOct2023.Data;
using ThesisOct2023.Models;

namespace ThesisOct2023.Repositories
{
    public class FoodRepository : IFoodRepository
    {
       
        private ApplicationDbContext context;
        public FoodRepository(ApplicationDbContext context)
        {
            
            this.context = context;
        }

        public IEnumerable<Food> GetAllFood()
        {
            return context.Foods.ToList();
        }

        public Food GetFoodById(int id)
        {
            return context.Foods.Find(id);
        }
        public void AddFood(FoodHelper foodhelper)
        {
            Food food = new Food() {
                Title= foodhelper.Title,
                Description= foodhelper.Description,
                Category= foodhelper.Category,
                ImageUrl = foodhelper.ImageUrl
            };
            context.Foods.Add(food);
            context.SaveChanges();
        }
    }
}
