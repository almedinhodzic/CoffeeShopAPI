using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeShopAPI.Data;
using CoffeeShopAPI.Models.Products;
using AutoMapper;
using CoffeeShopAPI.IRepository;

namespace CoffeeShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductsController(
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper,
            IProductRepository productRepository)
        {
            this.webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            
            return _mapper.Map<List<Product>, List<ProductDto>>(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsDto>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductDetailsAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody]UpdateProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }

            if(!await ProductExists(id))
            {
                return NotFound();
            }

            if (productDto.ImageFile != null)
            {
                productDto.ImageName = await SaveImage(productDto.ImageFile);
            }

            var product = _mapper.Map<UpdateProductDto, Product>(productDto);

            await _productRepository.UpdateAsync(product);

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDetailsDto>> CreateProduct(CreateProductDto productDto)
        {

            if (productDto.ImageFile != null)
            {
                productDto.ImageName = await SaveImage(productDto.ImageFile);
            }

            var product = _mapper.Map<CreateProductDto, Product>(productDto);
            await _productRepository.AddAsync(product);

            return CreatedAtAction(
                "GetProduct",
                new { id = product.Id },
                _mapper.Map<Product, ProductDetailsDto>(product));
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!await ProductExists(id))
            {
                return NotFound();
            }

            await _productRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _productRepository.Exists(id);
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }
    }
}
