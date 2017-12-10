namespace SIENN.Services.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using Type = Model.Type;

    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Code { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime DeliveryDate { get; set; }
        public ICollection<CategoryToProduct> CategoryToProducts { get; set; }
        public Model.Type Type { get; set; }
        public int TypeId { get; set; }
        public Unit Unit { get; set; }
        public int UnitId { get; set; }

        public Product()
        {
            CategoryToProducts = new Collection<CategoryToProduct>();
        }
    }
}