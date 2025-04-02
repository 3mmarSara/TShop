using Mapster;
using Microsoft.AspNetCore.Mvc;
using TShop.API.DTOs.Requests;
using TShop.API.DTOs.Responses;
using TShop.API.Models;
using TShop.API.Services;

namespace TShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var categories = _categoryService.GetAll();
            return Ok(categories.Adapt<IEnumerable<CategoryResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var category = _categoryService.Get(c => c.Id == id);
            return category == null? NotFound() : Ok(category.Adapt<CategoryResponse>());
        }

        [HttpPost("")]
        public IActionResult Create([FromBody]CategoryRequest category)
        {
            var addedCategory =  _categoryService.Add(category.Adapt<Category>());
            //return Created($"{Request.Scheme}://{Request.Host}/api/Categories/{category.Id}", category);
            return CreatedAtAction(nameof(GetById), new { id = addedCategory.Id }, addedCategory);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
          
            var isCategoryDeleted = _categoryService.Remove(id);
            return isCategoryDeleted ? NoContent() : NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]int id, [FromBody]CategoryRequest updatedCategory)
        {

            var isCategoryUpdated = _categoryService.Edit(id, updatedCategory.Adapt<Category>());
            return isCategoryUpdated ? NoContent() : NotFound();
        }

    }
}
