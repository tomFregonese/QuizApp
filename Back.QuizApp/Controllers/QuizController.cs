using Microsoft.AspNetCore.Mvc;
using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;
using Ynov.QuizApp.Controllers.Mappers;

namespace Ynov.QuizApp.Controllers {
    
    [Route("/v1/quizzes")]
    [ApiController]
    public class QuizController(IQuizService _service, QuizMapper _mapper) : ControllerBase {
        
        [HttpGet(Name = "GetAllQuiz")]
        [ProducesResponseType(typeof(IEnumerable<QuizDTO>), StatusCodes.Status200OK)]
        public IActionResult GetAll() {
            IEnumerable<QuizDTO> dtos = _service.GetAll().Select(quiz => _mapper.ToDto(quiz));
            return Ok(dtos);
        }


        [HttpGet("{id}", Name = "GetAQuiz")]
        [ProducesResponseType(typeof(QuizDTO), StatusCodes.Status200OK)]
        public IActionResult GetQuiz(Guid id) {
            Quiz? quiz = _service.GetQuizById(id);
            
            if (quiz == null) {
                return NotFound();
            }
            
            QuizDTO quizDto = _mapper.ToDto(quiz);
            return Ok(quizDto);
        }
    
    }
    
}
