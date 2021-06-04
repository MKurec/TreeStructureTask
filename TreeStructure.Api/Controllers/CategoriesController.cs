using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStructure.Infrastructure.Commands;
using TreeStructure.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace TreeStructure.Api.Controllers
{
    [Route("[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var categories = await _categoryService.BrowseAsync();
            return Json(categories);
        }
        [HttpGet("Tree")]
        public async Task<IActionResult> Get(bool a)
        {

            var categories = await _categoryService.CategoryTreeAsync();
            return Json(categories);
        }
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> Get(Guid categoryId)
        {
            var @category = await _categoryService.GetAsync(categoryId);
            if (@category == null)
            {
                return NotFound();
            }
            return Json(@category);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddCategory command)
        {
            var Id = Guid.NewGuid();
            await _categoryService.CreateAsync(Id, command.name);
            return Created($"/category/{Id}", null);
        }
        [HttpPost("{parentCategoryId}")]
        public async Task<IActionResult> Post(Guid parentCategoryId, [FromBody] AddCategory command)
        {
            var Id = Guid.NewGuid();
            await _categoryService.AddSubCategory(parentCategoryId, Id, command.name);
            return Created($"/category/{Id}", null);
        }
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> Put(Guid categoryId, [FromBody] AddCategory command)
        {
            await _categoryService.UpdateAsync(categoryId, command.name);
            return Created($"/category/{categoryId}", null);
        }
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            await _categoryService.DeleteAsync(categoryId);
            return NoContent();
        }
    }
}