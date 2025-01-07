using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;

namespace Ynov.QuizApp.Controllers.Mappers;

public class QuestionIndexAndIdMapper {
    public QuestionIndexAndIdDTO ToDto(QuestionIndexAndId questionIndexAndId) {
        return new QuestionIndexAndIdDTO() {
            QuestionId = questionIndexAndId.QuestionId,
            TotalNumberOfQuestions = questionIndexAndId.TotalNumberOfQuestions,
            CurrentQuestionIndex = questionIndexAndId.CurrentQuestionIndex
        }; 
    }
}