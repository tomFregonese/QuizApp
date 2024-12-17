using Microsoft.AspNetCore.Mvc;
using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;
using Ynov.QuizApp.Controllers.Mappers;

namespace Ynov.QuizApp.Controllers {
    
    [Route("/v1/")]
    [ApiController]
    public class UserController(IUserService _service, UserMapper _mapper) : ControllerBase {
        
        [HttpGet("user/{email}", Name = "GetAUser")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        public IActionResult GetAUser(string email) {
            User user = _service.GetUserByEmail(email);
            
            if (user.isEmpty()) {
                return NotFound();
            }
            
            UserDTO userDto = _mapper.ToDto(user);
            return Ok(userDto);
        }
    
    }
    
}
