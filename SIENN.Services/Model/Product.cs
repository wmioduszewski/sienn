namespace SIENN.WebApi.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime DeliveryDate { get; set; }
        public ICollection<Category> Categories { get; set; }
        public Type Type { get; set; }
        public Unit Unit { get; set; }

        public Product()
        {
            Categories = new Collection<Category>();
        }
    }
}