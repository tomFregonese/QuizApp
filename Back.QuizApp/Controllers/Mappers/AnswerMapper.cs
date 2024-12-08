using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;

namespace Ynov.QuizApp.Controllers.Mappers;

public class AnswerMapper {
    public AnswerDTO ToDto(Question question) {
        return new AnswerDTO() {
            CorrectOptionIndices = question.CorrectOptionIndices,
        }; 
    }
}