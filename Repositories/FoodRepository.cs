using NuGet.Packaging.Signing;
using System.Drawing;
using ThesisOct2023.Data;
using ThesisOct2023.Models;
using ThesisOct2023.Models.ViewModels;

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
                healthValue=foodhelper.healthValue,
                ImageUrl = foodhelper.ImageUrl
            };
            context.Foods.Add(food);
            context.SaveChanges();
        }
        public IEnumerable<Food> getFoodOfDay(int day, int week)
        {
            var query = from f in context.Foods
                        join m_item in context.MenuItems on f.Id equals m_item.FoodId
                        where m_item.Day == day
                        join m in context.Menus on m_item.MenuId equals m.Id
                        where m.week == week
                        select f;
            return query.ToList();
        }

        public IEnumerable<Food> getFoodSevedNow(string timeOfDay, int week, int day)
        {
            var query = from f in context.Foods
                        join m_item in context.MenuItems on f.Id equals m_item.FoodId
                        where m_item.ServedAt.ToUpper() == timeOfDay.ToUpper()
                        where m_item.Day == day
                        join m in context.Menus on m_item.MenuId equals m.Id
                        where m.week == week
                        select f;
            return query.ToList();
        }
        public IEnumerable<Food> getFoodByWeek(int week)
        {
            var query = from f in context.Foods
                        join m_item in context.MenuItems on f.Id equals m_item.FoodId
                        join m in context.Menus on m_item.MenuId equals m.Id
                        where m.week == week
                        select f;
            return query.ToList();
        }
        public void updateFoodRating(Food food,float rating)
        {
            //Get number of reviews for this food
            int howmany = context.Reviews.Where(r => r.FoodId == food.Id).Count();
            var r = food.AvgRating;
            //if no reviews yet
            if (r == null)
            {
                food.AvgRating = rating;
            }
            else
            {
                double res = (double)((r * howmany + rating) / (howmany + 1));
                food.AvgRating = res;
            }
            
            context.SaveChanges();
        }

        public IEnumerable<Food> getAllByCategory(string category)
        {
            var query = context.Foods
                .Where(f => f.Category.ToUpper() == category.ToUpper())
                .OrderByDescending(f => f.AvgRating+ f.healthValue)
                .ToList();
           
            return query;
        }

        public void Enable(int foodId)
        {
            Food food1 = context.Foods.Find(foodId);
            food1.Enabled = true;
            context.Foods.Update(food1);
            context.SaveChanges();
        }

        public void Disable(int foodId)
        {
            Food food1 = context.Foods.Find(foodId);
            food1.Enabled = false;
            context.Foods.Update(food1);
            context.SaveChanges();
        }
        public List<Food> getAllEnabledFood()
        {
            return context.Foods.Where(f => f.Enabled == true).ToList();
        }
        public List<FoodChartViewModel> GetFoodCharts()
        {
            List<FoodChartViewModel> list= new List<FoodChartViewModel>();
            IEnumerable<Food> foods = context.Foods.ToList();
            foreach(Food f in foods)
            {
                var answers = from r in context.Reviews
                              join rq in context.ReviewQuestions on r.Id equals rq.ReviewId
                              where r.FoodId == f.Id
                              select rq;
                if (answers.Any())
                {
                    list.Add(new FoodChartViewModel()
                    {
                        Id = f.Id,
                        Title = f.Title,
                        ones = answers.Where(a => a.Answer == 1).Count(),
                        twes = answers.Where(a => a.Answer == 2).Count(),
                        threes = answers.Where(a => a.Answer == 3).Count(),
                        fours = answers.Where(a => a.Answer == 4).Count(),
                        fives = answers.Where(a => a.Answer == 5).Count()

                    });
                }
                
            }
            return list;
            
        }

        public List<FoodChartViewModel> GetFoodChartsContainingTerm(string term)
        {
           var list = new List<FoodChartViewModel>();
            var foods = (from f in context.Foods
                            where term == "" || f.Title.StartsWith(term)
                            select new Food
                            {
                                Id = f.Id,
                                Title = f.Title,
                                Description = f.Description
                            });
            
            foreach (Food f in foods)
            {
                var answers = from r in context.Reviews
                                join rq in context.ReviewQuestions on r.Id equals rq.ReviewId
                                where r.FoodId == f.Id
                                select rq;
                if (answers.Any())
                {
                    list.Add(new FoodChartViewModel()
                    {
                        Id = f.Id,
                        Title = f.Title,
                        ones = answers.Where(a => a.Answer == 1).Count(),
                        twes = answers.Where(a => a.Answer == 2).Count(),
                        threes = answers.Where(a => a.Answer == 3).Count(),
                        fours = answers.Where(a => a.Answer == 4).Count(),
                        fives = answers.Where(a => a.Answer == 5).Count()

                    });
                }

            }
            return list;
        }
        
    }
}
