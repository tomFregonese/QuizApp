using Microsoft.AspNetCore.Mvc;
using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;
using Ynov.QuizApp.Controllers.Mappers;

namespace Ynov.QuizApp.Controllers;

[Route("/v1/")]
[ApiController]
public class CategoryController(ICategoryService _service, CategoryMapper _mapper) : ControllerBase  {
        
    [HttpGet("fetch-categories/", Name = "FetchCategories")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDTO>), StatusCodes.Status200OK)]
    public IActionResult FetchCategories(Guid categoryId) {
        IEnumerable<CategoryDTO> dtos = _service.GetAllCategories().Select(category => _mapper.ToDto(category));
        
        return Ok(dtos);
    }
    
}
