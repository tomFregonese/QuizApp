using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public interface IUserQuizProgressService {
    
    Boolean IsQuizStarted(Guid userId, Guid quizId);
    Boolean IsQuizCompleted(Guid userId, Guid quizId);
    Boolean StartAQuiz(Guid userId, Guid quizId);
    QuestionIndexAndId GetCurrentQuestion(Guid userId, Guid quizId);
}