using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public interface IQuestionService {
    
    IEnumerable<Question> GetQuestionsByQuizId(Guid quizId);
    Question GetAnswersByQuestionId(Guid questionId);
    
}