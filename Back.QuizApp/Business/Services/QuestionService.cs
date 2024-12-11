using System.Text.Json;
using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public class QuestionService : IQuestionService {

    private readonly string _dataFilePath = "DataSet/questions-table.json";
    
    public IEnumerable<Question> GetQuestionsByQuizId(Guid quizId) {
        var jsonData = File.ReadAllText(_dataFilePath);
        var questions = JsonSerializer.Deserialize<List<Question>>(jsonData) ?? new List<Question>();
        return questions.FindAll(q => q.QuizId == quizId);
    }
    
    public Question GetAnswersByQuestionId(Guid id) {
        var jsonData = File.ReadAllText(_dataFilePath);
        var data = JsonSerializer.Deserialize<List<Question>>(jsonData);
        var questions = data ?? new List<Question>();
        return questions.FirstOrDefault(q => q.Id == id) ?? new Question();
    }

}