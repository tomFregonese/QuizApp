using Microsoft.AspNetCore.Mvc;
using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;
using Ynov.QuizApp.Controllers.Mappers;

namespace Ynov.QuizApp.Controllers;

[Route("/v1/")]
[ApiController]
public class QuestionController(IQuestionService _service, 
                                QuestionMapper _questionMapper, 
                                AnswerMapper _answerMapper) : ControllerBase  {
        
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

    [HttpGet("answer/{QuestionId}", Name = "GetAnswersByQuestionId")]
    [ProducesResponseType(typeof(AnswerDTO), StatusCodes.Status200OK)]
    public IActionResult GetAnswersByQuestionId(Guid QuestionId) {
        AnswerDTO dto = _answerMapper.ToDto(_service.GetAnswersByQuestionId(QuestionId));
        
        if (dto.CorrectOptionIndices == null) {
            return NotFound();
        }
        
        return Ok(dto);
        
    }
    
}
