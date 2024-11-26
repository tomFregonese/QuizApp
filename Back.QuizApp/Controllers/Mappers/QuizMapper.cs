using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;

namespace Ynov.QuizApp.Controllers.Mappers;

public class QuizMapper {
    public QuizDTO ToDto(Quiz quiz) {
        return new QuizDTO() {
            Id = quiz.Id,
            Description = quiz.Description,
            Difficulty = quiz.Difficulty,
            Name = quiz.Name,
            CategoryId = quiz.Category.Id,
            CreationDate = quiz.CreationDate
        }; 
    }
}