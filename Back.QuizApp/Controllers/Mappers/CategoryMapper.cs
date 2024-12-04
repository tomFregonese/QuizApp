using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;

namespace Ynov.QuizApp.Controllers.Mappers;

public class CategoryMapper {
    public CategoryDTO ToDto(Category category) {
        return new CategoryDTO() {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
        }; 
    }
}