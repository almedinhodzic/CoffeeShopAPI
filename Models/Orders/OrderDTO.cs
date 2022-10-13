namespace CoffeeShopAPI.Models.Orders
{
    public class OrderDTO
    {
        public class Detail
        {
            public int ProductId { get; set; }
            public string? ProductName { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
        public IEnumerable<Detail>? Details { get; set; }
        public int OrderId { get; set; }
        public int TableNumber { get; set; }
    }
}
