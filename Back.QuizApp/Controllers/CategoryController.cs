using Microsoft.AspNetCore.Mvc;
using Ynov.QuizApp.Business.Models;
using Ynov.QuizApp.Controllers.DTOs;
using Ynov.QuizApp.Controllers.Mappers;

namespace Ynov.QuizApp.Controllers;

[Route("/v1/")]
[ApiController]
public class CategoryController(ICategoryService _service, CategoryMapper _mapper) : ControllerBase  {
        
    [HttpGet("category/{categoryId}", Name = "GetCategoryById")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult GetCategoryById(Guid categoryId) {
        Category? category = _service.GetCategoryById(categoryId);
        
        if (category == null) {
            return NotFound();
        }
        
        return Ok(category);
    }

    [HttpGet("fetch-categories/", Name = "FetchCategories")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDTO>), StatusCodes.Status200OK)]
    public IActionResult FetchCategories(Guid categoryId) {
        Console.WriteLine("Tes morts");
        IEnumerable<CategoryDTO> dtos = _service.GetAllCategories().Select(category => _mapper.ToDto(category));
        
        return Ok(dtos);
    }
}
