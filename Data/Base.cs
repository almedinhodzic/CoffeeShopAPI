﻿namespace CoffeeShopAPI.Data
{
    public abstract class Base
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
