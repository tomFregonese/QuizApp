namespace Ynov.QuizApp.Business.Models;

public class UserQuizProgress {
    public Guid UserId { get; set; } 
    public Guid QuizId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public QuizStatus Status { get; set; }
    public List<int> GivenAnswers { get; set; } = new List<int>();
}

public enum QuizStatus {
    Started, 
    Completed
}
