namespace CoffeeShopAPI.Data
{
    public class Order : Base
    {
        public string? Note { get; set; }
        public int TableNumber { get; set; }
        public decimal TotalCost { get; set; }
        public List<Product>? Products { get; set; }
    }
}
