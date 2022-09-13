using AutoMapper;
using CoffeeShopAPI.Data;
using CoffeeShopAPI.IRepository;
using CoffeeShopAPI.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopAPI.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, 
            IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDetailsDto?> GetProductDetailsAsync(int id)
        {
            var entity = await _context.Products.Include(q => q.Category).FirstOrDefaultAsync();

            if (entity == null)
            {
                return null;
            }

            return _mapper.Map<Product, ProductDetailsDto>(entity);
        }
    }
}
