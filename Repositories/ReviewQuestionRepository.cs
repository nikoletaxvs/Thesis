using Microsoft.EntityFrameworkCore;
using ThesisOct2023.Data;
using ThesisOct2023.Models;

namespace ThesisOct2023.Repositories
{
    public class ReviewQuestionRepository : IReviewQuestionRepository
    {
        private readonly ApplicationDbContext context;
        public ReviewQuestionRepository(ApplicationDbContext context)
        {
            this.context = context; 
        }
        public void AddReviewQuestion(ReviewQuestion reviewQuestion)
        {
            context.ReviewQuestions.Add(reviewQuestion);
            context.SaveChanges();
        }

       //public List<string> getDistinctCommentByReviewId(int reviewId)
       // {
       //     var query = context.ReviewQuestions.Where(r => r.ReviewId ==reviewId).Select(r => r.Comment).Distinct();
       //     return query.ToList();
       // }
        public List<string> getDistinctCommentByFoodId(int foodId)
        {
            var query = context.Reviews.Where(r => r.FoodId == foodId).ToList();

            List<string> comments = new List<string>();
            foreach(var r in query)
            {
                ReviewQuestion reviewQuestion = context.ReviewQuestions.Where(rq => rq.ReviewId == r.Id).FirstOrDefault();
                if (reviewQuestion.Comment != null)
                {
                    comments.Add(reviewQuestion.Comment);

                }
            }


            return comments;
        }
        public void getScoreByReviewId(int reviewId)
        {

        }
    }
}
