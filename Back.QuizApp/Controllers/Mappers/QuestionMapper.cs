using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;

namespace Ynov.QuizApp.Controllers.Mappers;

public class QuestionMapper {
    public QuestionDTO ToDto(Question question) {
        return new QuestionDTO() {
            Id = question.Id,
            QuestionContent = question.QuestionContent,
            Options = question.Options
        }; 
    }
}