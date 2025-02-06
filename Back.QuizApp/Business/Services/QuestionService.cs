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
    
    public int GetQuestionIndex(Guid quizId, Guid questionId) {
        var quizData = File.ReadAllText(_quizzesFilePath);
        var quizzes = JsonSerializer.Deserialize<List<Quiz>>(quizData) ?? new List<Quiz>();
        var quiz = quizzes.FirstOrDefault(q => q.Id == quizId);
        return quiz.QuestionIds.IndexOf(questionId);
    }
    
    public List<Question> GetAllQuestions() {
        var questionData = File.ReadAllText(_questionsFilePath);
        return JsonSerializer.Deserialize<List<Question>>(questionData) ?? new List<Question>();
    }

}