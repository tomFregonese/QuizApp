namespace Ynov.QuizApp.Business.Models;

public class User {
    public Guid Id { get; set; } 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public Boolean isEmpty() {
        return this.Id == new Guid("00000000-0000-0000-0000-000000000000"); 
    }
}