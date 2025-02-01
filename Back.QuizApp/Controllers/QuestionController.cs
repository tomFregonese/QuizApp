using Microsoft.AspNetCore.Mvc;
using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;
using Ynov.QuizApp.Controllers.Mappers;

namespace Ynov.QuizApp.Controllers;

[Route("/v1/")]
[ApiController]
public class QuestionController(IQuestionService _service, 
                                QuestionMapper _questionMapper) : ControllerBase  {
        
    [HttpGet("question/{QuestionId}", Name = "GetQuestionById")]
    [ProducesResponseType(typeof(QuestionDTO), StatusCodes.Status200OK)]
    public IActionResult GetQuestionsByQuizId(Guid QuestionId) {
        Question question = _service.GetQuestionById(QuestionId);
        
        if (question.isEmpty()) {
            return NotFound();
        }
        
        QuestionDTO dto = _questionMapper.ToDto(question);
        
        return Ok(dto);
        
    }
    
}
