using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public interface IQuizService {
    IEnumerable<Quiz> GetAllQuizzes();
    Quiz? GetQuizById(Guid id);
}