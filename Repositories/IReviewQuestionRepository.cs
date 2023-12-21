using ThesisOct2023.Models;

namespace ThesisOct2023.Repositories
{
    public interface IReviewQuestionRepository
    {
        public void AddReviewQuestion(ReviewQuestion reviewQuestion);
        //public List<string> getDistinctCommentByReviewId(int reviewId);
        public List<string> getDistinctCommentByFoodId(int foodId);
    }
}
