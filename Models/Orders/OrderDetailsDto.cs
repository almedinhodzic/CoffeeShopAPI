namespace CoffeeShopAPI.Models.Orders
{
    public class OrderDetailsDto : BaseOrderDto
    {
        public int Id { get; set; }
        public decimal TotalCost { get; set; }
    }
}
