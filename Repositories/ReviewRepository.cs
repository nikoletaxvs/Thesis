using ThesisOct2023.Models;
using ThesisOct2023.Data;
namespace ThesisOct2023.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext context;
        public ReviewRepository(ApplicationDbContext context) {
            this.context = context;
        }
        public void AddReview(Review review)
        {
            context.Reviews.Add(review);
            context.SaveChanges();

            
        }
        public IEnumerable<int> GetStudentReviewFoodId(string studentId)
        {
            var query= from review in context.Reviews where review.StudentId == studentId select review.FoodId;
            return query.ToList();
        }

        public IEnumerable<Review> GetReviews()
        {
            return context.Reviews.ToList();
        }
    }
}
