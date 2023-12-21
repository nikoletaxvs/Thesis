using ThesisOct2023.Models;

namespace ThesisOct2023.Repositories
{
    public interface IReviewRepository
    {
        public void AddReview(Review review);
        public IEnumerable<int> GetStudentReviewFoodId(string studentId);
        public IEnumerable<Review> GetReviews();
        public List<Review> GetReviewsByFoodId(int foodId);
    }
}
