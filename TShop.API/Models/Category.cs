﻿namespace TShop.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
