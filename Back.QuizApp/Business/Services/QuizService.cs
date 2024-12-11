using System.Text.Json;
using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public class QuizService : IQuizService {

    private readonly string _dataFilePath = "DataSet/quizzes-table.json";

    public IEnumerable<Quiz> GetAllQuizzes() {
        var jsonData = File.ReadAllText(_dataFilePath);
        var quizzes = JsonSerializer.Deserialize<List<Quiz>>(jsonData) ?? new List<Quiz>();
        return quizzes;
    }

    public Quiz GetQuizById(Guid id) {
        var jsonData = File.ReadAllText(_dataFilePath);
        var quizzes = JsonSerializer.Deserialize<List<Quiz>>(jsonData) ?? new List<Quiz>();
        return quizzes.FirstOrDefault(q => q.Id == id, new Quiz());
    }

}