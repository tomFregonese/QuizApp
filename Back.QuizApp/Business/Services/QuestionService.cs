using System.Text.Json;
using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public class QuestionService : IQuestionService {

    private readonly string _questionsFilePath = "DataSet/questions-table.json";
    private readonly string _quizzesFilePath = "DataSet/quizzes-table.json";
    
    public IEnumerable<Question> GetQuestionsByQuizId(Guid quizId) {
        var quizData = File.ReadAllText(_quizzesFilePath);
        var quizzes = JsonSerializer.Deserialize<List<Quiz>>(quizData) ?? new List<Quiz>();
        var quiz = quizzes.FirstOrDefault(q => q.Id == quizId);

        if (quiz == null) {
            return new List<Question>();
        }

        var questionData = File.ReadAllText(_questionsFilePath);
        var questions = JsonSerializer.Deserialize<List<Question>>(questionData) ?? new List<Question>();
        return questions.Where(q => quiz.QuestionIds.Contains(q.Id)).ToList();
    }
    
    public Question GetAnswersByQuestionId(Guid questionId) {
        var jsonData = File.ReadAllText(_questionsFilePath);
        var data = JsonSerializer.Deserialize<List<Question>>(jsonData);
        var questions = data ?? new List<Question>();
        return questions.FirstOrDefault(q => q.Id == questionId) ?? new Question();
    }

}