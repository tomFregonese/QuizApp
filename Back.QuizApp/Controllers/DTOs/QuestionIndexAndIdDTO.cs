namespace Ynov.QuizApp.Controllers.DTOs;

public class QuestionIndexAndIdDTO {
    public Guid QuestionId { get; set; } 
    public int TotalNumberOfQuestions { get; set; }
    public int CurrentQuestionIndex { get; set; }
}