using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Services;
using SuperMarket.API.Resources;
using SuperMarket.API.Extensions;




namespace SuperMarket.API.Controllers
{
    [Produces("application/json")]
    [Route("/api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper) {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>IEnumerable CategoryRosource</returns>
        [HttpGet]
        public async Task<IEnumerable<CategoryResource>> GetAllAsync()
        {
            var categories = await _categoryService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);

            return resources;
        }

        /// <summary>
        /// Add new category item.
        /// </summary>
        /// <param name="resource">Category Object containing new details</param>
        /// <returns>IActionResult of CategoryReource</returns>
        /// <response code="400">Missing category object details</response>
        /// <response code="406">Not saved</response>
        /// <response code="200">New Category added</response>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCategoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = _mapper.Map<SaveCategoryResource, Category>(resource);
            var result = await _categoryService.SaveAsync(category);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
            return Ok(categoryResource);
        }

        /// <summary>
        /// Update a category item.
        /// </summary>
        /// <param name="id">ID of category item to be amended</param>
        /// <param name="resource">Category Object containing new details</param>
        /// <returns>IActionResult of CategoryReource</returns>
        /// <response code="400">Missing category object details</response>
        /// <response code="406">Not saved</response>
        /// <response code="200">New Category Details saved</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCategoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = _mapper.Map<SaveCategoryResource, Category>(resource);
            var result = await _categoryService.UpdateAsync(id, category);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
            return Ok(categoryResource);

        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="id">id of category to be deleted</param>
        /// <returns>IAction Result of object CategoryResource</returns>
        /// <response code="404">No Category found with specified id</response>
        /// <response code="406">Not saved</response>
        /// <response code="200">Category Deleted</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
            return Ok(categoryResource);
        }

    }
}
