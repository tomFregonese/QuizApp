using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public interface IQuestionService {
    
    Question GetQuestionById(Guid questionId);
    Question GetAnswersByQuestionId(Guid questionId);
    
}