using System.Text.Json;
using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public class UserService : IUserService {

    private readonly string _usersFilePath = "DataSet/users-table.json";
    private readonly string _usersQuizzesFilePath = "DataSet/user-quizzes-table.json";

    public User GetUserByEmail(string email) {
        var users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(_usersFilePath));
        var user = users.FirstOrDefault(u => u.Email == email);
        return user ?? new User();
    }
    
    public Boolean doesThisUserHaveAQuizInProgress(Guid userId) {
        var usersQuizzesProgress = JsonSerializer.Deserialize<List<UserQuizProgress>>(File.ReadAllText(_usersQuizzesFilePath));
        var userQuiz = usersQuizzesProgress.Where(uqp => uqp.UserId == userId && uqp.Status == QuizStatus.Started);
        
        if (userQuiz == null) {
            return false;
        }
        
        return true;
    }

}