using System.Drawing;
using ThesisOct2023.Data;
using ThesisOct2023.Models;

namespace ThesisOct2023.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        List<Food> FoodList;
        private ApplicationDbContext context;
        public FoodRepository(ApplicationDbContext context)
        {
            //FormFile photo = ".../wwwroot/lib/images/bread.jpg";
            
            FoodList = new List<Food>()
            {
                new Food{ Id = 1,Title="Freshly baked bread",Description="Very fresh bread....",Day="1",Category="Appetizer"}
            };
            this.context = context;
        }

        public IEnumerable<Food> GetAllFood()
        {
            throw new NotImplementedException();
        }

        public Food GetFoodById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
