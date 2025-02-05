using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;

namespace Ynov.QuizApp.Controllers;

public interface IUserQuizProgressService {
    Boolean IsQuizStarted(Guid userId, Guid quizId); //Used to either display quiz infos or display quiz questions on the front 
    Boolean IsQuizCompleted(Guid userId, Guid quizId);
    Boolean StartAQuiz(Guid userId, Guid quizId);
    QuestionIndexAndId GetCurrentQuestion(Guid userId, Guid quizId);
    Boolean AnswerQuestion(Guid userId, Guid quizId, Guid questionId, List<int> selectedOptions);
    Answer GetAnswersByQuestionId(Guid userId, Guid quizId, Guid questionId);
    void CloseUnfinishedQuizzes();
}