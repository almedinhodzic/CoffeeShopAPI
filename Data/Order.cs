namespace CoffeeShopAPI.Data
{
    public class Order : Base
    {
        public string? Note { get; set; }
        public int TableNumber { get; set; }
        public ICollection<OrderProduct>? OrderProducts { get; set; }
        
    }
}
