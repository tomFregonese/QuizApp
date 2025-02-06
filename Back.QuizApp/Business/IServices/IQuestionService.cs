using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public interface IQuestionService {
    Question GetQuestionById(Guid questionId);
    int GetQuestionIndex(Guid quizId, Guid questionId);
    List<Question> GetAllQuestions(); 
}