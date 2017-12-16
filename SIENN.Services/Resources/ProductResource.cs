using System;
using System.Collections.Generic;
using System.Text;

namespace SIENN.Services.Resources
{
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    public class ProductResource
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
        public ICollection<int> Categories { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public int UnitId { get; set; }

        public ProductResource()
        {
            Categories = new Collection<int>();
        }
    }
}
