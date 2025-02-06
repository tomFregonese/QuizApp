using Microsoft.AspNetCore.Mvc;
using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;
using Ynov.QuizApp.Controllers.Mappers;

namespace Ynov.QuizApp.Controllers {
    
    [Route("/v1/")]
    [ApiController]
    public class UserQuizProgressController(IUserQuizProgressService _service, 
                                            AnswerMapper _answerMapper) : ControllerBase {
        
        [HttpGet("get-quiz-status/{userId}/{quizId}", Name = "GetQuizStatus")]
        [ProducesResponseType(typeof(QuizStatus), StatusCodes.Status200OK)]
        public IActionResult GetQuizStatus(Guid userId, Guid quizId) {
            return Ok(_service.GetQuizStatus(userId, quizId));
        }
        
        [HttpPost("start-quiz/{userId}/{quizId}", Name = "StartAQuiz")]
        [ProducesResponseType(typeof(Boolean), StatusCodes.Status200OK)]
        public IActionResult StartAQuiz(String userId, String quizId) {
            bool result = _service.StartAQuiz(new Guid(userId), new Guid(quizId));
            if (!result) {
                return BadRequest();
            }
            return Ok();
        }
        
        [HttpGet("get-current-question/{userId}/{quizId}", Name = "GetCurrentQuestion")]
        [ProducesResponseType(typeof(QuestionIndexAndIdDTO), StatusCodes.Status200OK)]
        public IActionResult GetCurrentQuestion(String userId, String quizId) {
            return Ok(_service.GetCurrentQuestion(new Guid(userId), new Guid(quizId)));
        }
        
        [HttpPost("answer-question/{userId}/{quizId}/{questionId}", Name = "AnswerQuestion")]
        [ProducesResponseType(typeof(Boolean), StatusCodes.Status200OK)]
        public IActionResult AnswerQuestion(String userId, String quizId, String questionId, [FromBody] List<int> 
            selectedOptions) {
            Boolean response = _service.AnswerQuestion(new Guid(userId), new Guid(quizId), 
                new Guid(questionId), selectedOptions);
            if (response == false) {
                return BadRequest();
            }
            return Ok(response);
        }
        
        [HttpGet("get-answer/{userId}/{quizId}/{questionId}", Name = "GetAnswersByQuestionId")]
        [ProducesResponseType(typeof(AnswerDTO), StatusCodes.Status200OK)]
        public IActionResult GetAnswersByQuestionId(Guid userId, Guid quizId, Guid questionId) {
            AnswerDTO dto = _answerMapper.ToDto(_service.GetAnswersByQuestionId(userId, quizId, questionId));
        
            if (dto.CorrectOptionIndices == null) {
                return NotFound();
            }
        
            return Ok(dto);
        
        }
    }
    
}
