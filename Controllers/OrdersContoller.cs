using AutoMapper;
using CoffeeShopAPI.IRepository;
using CoffeeShopAPI.Models.Orders;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class OrdersContoller : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrdersContoller(IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetailsDto>> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /*            var order = _mapper.Map<CreateOrderDto, Order>(createOrderDto);
                        var record = await _orderRepository.AddAsync(order);*/

            return Ok();
        }
    }
}
