﻿using CoffeeShopAPI.Data;
using CoffeeShopAPI.IRepository;

namespace CoffeeShopAPI.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
