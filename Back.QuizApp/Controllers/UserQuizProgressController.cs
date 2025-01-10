using Microsoft.AspNetCore.Mvc;
using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;
using Ynov.QuizApp.Controllers.Mappers;

namespace Ynov.QuizApp.Controllers {
    
    [Route("/v1/")]
    [ApiController]
    public class UserQuizProgressController(IUserQuizProgressService _service) : ControllerBase {
        
        [HttpGet("is-quiz-started/{userId}/{quizId}", Name = "IsQuizStarted")]
        [ProducesResponseType(typeof(Boolean), StatusCodes.Status200OK)]
        public IActionResult IsQuizStarted(Guid userId, Guid quizId) {
            return Ok(_service.IsQuizStarted(userId, quizId));
        }
        
        [HttpGet("is-quiz-completed/{userId}/{quizId}", Name = "IsQuizCompleted")]
        [ProducesResponseType(typeof(Boolean), StatusCodes.Status200OK)]
        public IActionResult IsQuizCompleted(Guid userId, Guid quizId) {
            return Ok(_service.IsQuizCompleted(userId, quizId));
        }
        
        [HttpPost("start-quiz/{userId}/{quizId}", Name = "StartAQuiz")]
        [ProducesResponseType(typeof(Boolean), StatusCodes.Status200OK)]
        public IActionResult StartAQuiz(String userId, String quizId) {
            return Ok(_service.StartAQuiz(new Guid(userId), new Guid(quizId)));
        }
        
        [HttpGet("get-current-question/{userId}/{quizId}", Name = "GetCurrentQuestion")]
        [ProducesResponseType(typeof(QuestionIndexAndIdDTO), StatusCodes.Status200OK)]
        public IActionResult GetCurrentQuestion(String userId, String quizId) {
            return Ok(_service.GetCurrentQuestion(new Guid(userId), new Guid(quizId)));
        }
        
        [HttpPost("answer-question/{userId}/{questionId}", Name = "AnswerQuestion")]
        [ProducesResponseType(typeof(Boolean), StatusCodes.Status200OK)]
        public IActionResult AnswerQuestion(String userId, String questionId, [FromBody] List<int> selectedOptions) {
            Boolean response = _service.AnswerQuestion(new Guid(userId), new Guid(questionId), selectedOptions);
            if (response == false) {
                return BadRequest();
            }
            return Ok(response);
        }
    }
    
}
