namespace Ynov.QuizApp.Business.Models;

public class Question {
    
    public Guid Id { get; set; }
    public string QuestionContent { get; set; }
    public List<string> Options { get; set; }
    public List<int> CorrectOptionIndices { get; set; }
    
}