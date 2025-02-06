using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;

namespace Ynov.QuizApp.Controllers;

public interface IUserQuizProgressService {
    QuizStatus GetQuizStatus(Guid userId, Guid quizId);
    Boolean StartAQuiz(Guid userId, Guid quizId);
    QuestionIndexAndId GetCurrentQuestion(Guid userId, Guid quizId);
    Boolean AnswerQuestion(Guid userId, Guid quizId, Guid questionId, List<int> selectedOptions);
    Answer GetAnswersByQuestionId(Guid userId, Guid quizId, Guid questionId);
    void CloseUnfinishedQuizzes();
}