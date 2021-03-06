using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStructure.Infrastructure.Commands;
using TreeStructure.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace TreeStructure.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "HasAdminRole")]
    public class CategoryNodesController : DefaultController
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ICategoryService _categoryService;

        public CategoryNodesController(ICategoryService categoryService,IWebHostEnvironment environment)
        {
            _categoryService = categoryService;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }



        [HttpPut("SortNode/{categoryId}")]
        public async Task<IActionResult> Put(Guid categoryId, [FromBody] SortCategories command)
        {
            string path = _environment.WebRootPath.ToString();
            await _categoryService.SortCategories(command.Decending,command.SortSubcategories,categoryId,path);
            return StatusCode(200);
        }
        [HttpPut("SortNodes")]
        public async Task<IActionResult> Put([FromBody] SortCategories command)
        {
            string path = _environment.WebRootPath.ToString();
            await _categoryService.SortCategories(command.Decending, command.SortSubcategories, null, path);
            return StatusCode(200);
        }
        [HttpPut("MoveNode/{categoryId}")]
        public async Task<IActionResult> Put(Guid categoryId, [FromBody] MoveCategory command)
        {
            await _categoryService.MoveCategoryAsync(categoryId, command.Id);
            return Created($"/category/{categoryId}", null);
        }

    }
}