using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;

namespace Ynov.QuizApp.Controllers.Mappers;

public class UserMapper {
    public UserDTO ToDto(User user) {
        return new UserDTO() {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        }; 
    }
}