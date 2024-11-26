using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public interface IQuizService {
    IEnumerable<Quiz> GetAll();
    Quiz? GetQuizById(Guid id);
}