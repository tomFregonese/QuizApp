using System.Text.Json;
using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public class UserService : IUserService {

    private readonly string _dataFilePath = "DataSet/users-table.json";

    public User GetUserByEmail(string email) {
        var users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(_dataFilePath));
        var user = users.FirstOrDefault(u => u.Email == email);
        return user ?? new User();
    }

}