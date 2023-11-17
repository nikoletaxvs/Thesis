using ThesisOct2023.Models;

namespace ThesisOct2023.Repositories
{
    public interface IQuestionRepository
    {
        public List<Question> GetQuestions();
        public void AddQuestion(Question question);
        public Question GetQuestion(int? id);
        public void DeleteQuestion(Question question);
        public void UpdateQuestion(Question question);

	}
}
