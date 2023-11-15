using ThesisOct2023.Data;
using ThesisOct2023.Models;

namespace ThesisOct2023.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext context;
        public QuestionRepository(ApplicationDbContext context) {
            this.context= context;
        }
        public List<Question> GetQuestions()
        {
            return context.Questions.ToList();
        }
		public void AddQuestion(Question question)
        {
            context.Questions.Add(question);
            context.SaveChanges();
        }
		public Question GetQuestion(int? id)
        {
            return context.Questions.Find(id);
        }

		public void DeleteQuestion(Question question)
        {
            context.Questions.Remove(question);
            context.SaveChanges();
        }

	}
}
