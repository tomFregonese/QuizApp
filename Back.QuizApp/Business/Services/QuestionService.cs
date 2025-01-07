using System.Text.Json;
using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public class QuestionService : IQuestionService {

    private readonly string _questionsFilePath = "DataSet/questions-table.json";
    private readonly string _quizzesFilePath = "DataSet/quizzes-table.json";
    
    public Question GetQuestionById(Guid questionId) {
        var questionData = File.ReadAllText(_questionsFilePath);
        var questions = JsonSerializer.Deserialize<List<Question>>(questionData) ?? new List<Question>();
        return questions.FirstOrDefault(q => q.Id == questionId) ?? new Question();
    }
    
    public Question GetAnswersByQuestionId(Guid questionId) {
        var jsonData = File.ReadAllText(_questionsFilePath);
        var data = JsonSerializer.Deserialize<List<Question>>(jsonData);
        var questions = data ?? new List<Question>();
        return questions.FirstOrDefault(q => q.Id == questionId) ?? new Question();
    }

}