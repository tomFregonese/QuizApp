using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public class QuestionService : IQuestionService {

    private List<Question> _questions = new List<Question> {
        new Question {
            Id = new Guid("de497605-7681-4d7d-b3bd-f082c6c23b0b"),
            QuestionContent = "Which of the following is a valid option ?", 
            Options = new List<string> { "Option 1", "Option 2", "Option 3", "Option 4" },
            CorrectOptionIndices = new List<int> { 0 },
            QuizId = new Guid("ca655c54-0d8b-4ff1-9363-46f269bc5f71"),
        }, new Question {
            Id = new Guid("502aa918-321e-40d5-a276-39a3136610b4"),
            QuestionContent = "Another question", 
            Options = new List<string> { "Option 1", "Option 2", "Option 3", "Option 4" },
            CorrectOptionIndices = new List<int> { 3 },
            QuizId = new Guid("ca655c54-0d8b-4ff1-9363-46f269bc5f71"),
        }
    };

    public IEnumerable<Question> GetQuestionsByQuizId(Guid quizId) {
        return _questions.FindAll(q => q.QuizId == quizId);
    }
    
    public Question GetAnswersByQuestionId(Guid id) {
        return _questions.FirstOrDefault(q => q.Id == id)?? throw new ArgumentNullException(nameof(id), "Question not found");
    }

}