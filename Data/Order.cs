namespace CoffeeShopAPI.Data
{
    public class Order : Base
    {
        public decimal TotalCost { get; set; }
        public List<Product>? Products { get; set; }
    }
}
