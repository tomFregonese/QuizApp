namespace Ynov.QuizApp.Controllers.DTOs;

public class QuestionDTO {
    
    public Guid Id { get; set; }
    public string QuestionContent { get; set; }
    public List<string> Options { get; set; }
    
}