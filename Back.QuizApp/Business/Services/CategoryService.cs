using System.Text.Json;
using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public class CategoryService : ICategoryService {

    private readonly string _dataFilePath = "DataSet/categories-table.json";

    public IEnumerable<Category> GetAllCategories() {
        var jsonData = File.ReadAllText(_dataFilePath);
        var categories = JsonSerializer.Deserialize<List<Category>>(jsonData) ?? new List<Category>();
        return categories;
    }
    
}