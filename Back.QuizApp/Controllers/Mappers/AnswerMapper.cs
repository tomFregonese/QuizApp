using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;

namespace Ynov.QuizApp.Controllers.Mappers;

public class AnswerMapper {
    public AnswerDTO ToDto(Answer answer) {
        return new AnswerDTO() {
            CorrectOptionIndices = answer.CorrectOptionIndices,
        }; 
    }
}