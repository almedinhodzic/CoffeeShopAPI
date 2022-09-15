namespace CoffeeShopAPI.Models.Orders
{
    public class CreateOrderDto
    {
        public string? Note { get; set; }
        public int TableNumber { get; set; }
        List<int>? ProductsId { get; set; }
    }
}
