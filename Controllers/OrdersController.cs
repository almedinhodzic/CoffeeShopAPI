using AutoMapper;
using CoffeeShopAPI.Data;
using CoffeeShopAPI.Exceptions;
using CoffeeShopAPI.IRepository;
using CoffeeShopAPI.Models.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public OrdersController(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            var records = await _orderRepository.GetAllOrdersDetailsAsync();
            if (records == null)
            {
                throw new NotFoundException("", nameof(GetOrders));
            }

            return Ok(records);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var record = await _orderRepository.GetOrderDetailsAsync(id);
            if (record == null)
            {
                throw new NotFoundException("", nameof(GetOrders));
            }

            return Ok(record);
        }
    }
}
