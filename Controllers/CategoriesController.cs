using AutoMapper;
using CoffeeShopAPI.Data;
using CoffeeShopAPI.IRepository;
using CoffeeShopAPI.Models.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public readonly ICategoryRepository _categoryRepository;
        public readonly IMapper _mapper;
        public CategoriesController(ICategoryRepository categoryRepository, 
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            if(categories == null)
            {
                return NotFound();
            }

            var records = _mapper.Map<List<Category>, List<CategoryDto>>(categories);
            return Ok(records);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {

            var category = await _categoryRepository.GetAsync(id);

            if(category == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<Category, CategoryDto>(category);
            return Ok(record);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var category = _mapper.Map<CreateCategoryDto, Category>(createCategoryDto);

            var record = await _categoryRepository.AddAsync(category);
            return CreatedAtAction("GetCategory", new { id = category.Id }, record);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            if(id != updateCategoryDto.Id)
            {
                return BadRequest();
            }

            if(!await _categoryRepository.Exists(id))
            {
                return NotFound();
            }

            var category = _mapper.Map<UpdateCategoryDto, Category>(updateCategoryDto);

            await _categoryRepository.UpdateAsync(category);

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if(!await _categoryRepository.Exists(id))
            {
                return NotFound();
            }

            await _categoryRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
