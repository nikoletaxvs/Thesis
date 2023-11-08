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

       
    }
}
