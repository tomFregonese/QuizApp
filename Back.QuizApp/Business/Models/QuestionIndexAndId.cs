namespace Ynov.QuizApp.Business.Models;

public class QuestionIndexAndId {
    public Guid QuestionId { get; set; } 
    public int TotalNumberOfQuestions { get; set; }
    public int CurrentQuestionIndex { get; set; }
    
}