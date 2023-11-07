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
    }
}
