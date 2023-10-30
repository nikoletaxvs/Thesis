﻿using System.Drawing;
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
    }
}
