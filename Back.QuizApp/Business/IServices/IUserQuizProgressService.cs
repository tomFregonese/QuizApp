using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;

namespace Ynov.QuizApp.Controllers;

public interface IUserQuizProgressService {
    
    Boolean IsQuizStarted(Guid userId, Guid quizId);
    Boolean IsQuizCompleted(Guid userId, Guid quizId);
    Boolean StartAQuiz(Guid userId, Guid quizId);
    QuestionIndexAndId GetCurrentQuestion(Guid userId, Guid quizId);
    Boolean AnswerQuestion(Guid userId, Guid questionId, List<int> selectedOptions);
}