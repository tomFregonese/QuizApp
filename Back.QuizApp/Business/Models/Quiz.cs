namespace Ynov.QuizApp.Business.Models;

public class Quiz {
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Difficulty { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid CategoryId { get; set; }
    public List<Guid> QuestionIds { get; set; } = new List<Guid>();
    
    public Boolean isEmpty() {
        return this.Id == new Guid("00000000-0000-0000-0000-000000000000"); 
    }
    
}