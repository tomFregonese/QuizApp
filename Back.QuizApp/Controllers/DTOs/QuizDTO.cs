namespace Ynov.QuizApp.Controllers.DTOs;

public class QuizDTO {
    
    public Guid Id { get; set; }
    public string Name { get; set; } 
    public string Description { get; set; }
    public int Difficulty { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid? CategoryId { get; set; }
    public List<Guid> QuestionIds { get; set; } = new List<Guid>();
}