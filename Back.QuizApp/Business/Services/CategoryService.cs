using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public class CategoryService : ICategoryService {

    private List<Category> _categories = new List<Category> {
        new Category {
            Id = new Guid("83d82d07-1298-4a4b-9383-00eb985784b0"),
            Name = "Full-Stack"
        },
        new Category {
            Id = new Guid("83d82d07-1298-4a4b-9383-00eb985744b0"),
            Name = "Gestion de projet IT"
        }
    }; 

    public IEnumerable<Category> GetAllCategories() {
        return _categories;
    }
    
    public Category? GetCategoryById(Guid id) {
        return _categories.FirstOrDefault(c => c.Id == id);  
    }

}