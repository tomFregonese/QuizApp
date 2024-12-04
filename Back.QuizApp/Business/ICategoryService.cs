using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public interface ICategoryService {
    IEnumerable<Category> GetAllCategories();
    Category? GetCategoryById(Guid id);
}