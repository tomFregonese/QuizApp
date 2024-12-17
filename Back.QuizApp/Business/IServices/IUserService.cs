using Ynov.QuizApp.Business.Models;

namespace Ynov.QuizApp.Controllers;

public interface IUserService {
    
    User GetUserByEmail(string email);
    
}