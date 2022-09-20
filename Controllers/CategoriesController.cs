using AutoMapper;
using CoffeeShopAPI.Common;
using CoffeeShopAPI.Data;
using CoffeeShopAPI.Exceptions;
using CoffeeShopAPI.IRepository;
using CoffeeShopAPI.Models.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]

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
        [Authorize]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (categories == null)
            {
                throw new NotFoundException(nameof(GetCategories), "");
            }

            var records = _mapper.Map<List<Category>, List<CategoryDto>>(categories);
            return Ok(records);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {

            var category = await _categoryRepository.GetAsync(id);

            if (category == null)
            {
                throw new NotFoundException(nameof(GetCategory), id);
            }

            var record = _mapper.Map<Category, CategoryDto>(category);
            return Ok(record);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException(nameof(CreateCategory));
            }

            var category = _mapper.Map<CreateCategoryDto, Category>(createCategoryDto);

            var record = await _categoryRepository.AddAsync(category);
            return CreatedAtAction("GetCategory", new { id = category.Id }, record);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            if (id != updateCategoryDto.Id)
            {
                throw new BadRequestException(nameof(UpdateCategory));
            }

            if (!await _categoryRepository.Exists(id))
            {
                throw new NotFoundException(nameof(UpdateCategory), id);
            }

            var category = _mapper.Map<UpdateCategoryDto, Category>(updateCategoryDto);

            await _categoryRepository.UpdateAsync(category);

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!await _categoryRepository.Exists(id))
            {
                throw new NotFoundException(nameof(DeleteCategory), id);
            }

            await _categoryRepository.DeleteAsync(id);
            return NoContent();
        }

        // This action is to demonstrate versioning
        [HttpGet("GetCategoriesV2")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ApiVersion("2.0")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<List<CategoryDto>>> GetCategoriesV2()
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (categories == null)
            {
                throw new NotFoundException(nameof(GetCategoriesV2), "");
            }

            var records = _mapper.Map<List<Category>, List<CategoryDto>>(categories);
            foreach (var record in records)
            {
                record.Name = "V2";
            }
            return Ok(records);
        }

    }
}
