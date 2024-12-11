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
        
    [HttpGet("questions/{QuizId}", Name = "GetQuestionsByQuizId")]
    [ProducesResponseType(typeof(IEnumerable<QuestionDTO>), StatusCodes.Status200OK)]
    public IActionResult GetQuestionsByQuizId(Guid QuizId) {
        IEnumerable<QuestionDTO> questions = _service.GetQuestionsByQuizId(QuizId)
            .Select(question => _questionMapper.ToDto(question));
        
        if (!questions.Any()) {
            return NotFound();
        }
        
        return Ok(questions);
        
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
