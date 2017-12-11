using System;
using System.Collections.Generic;
using System.Text;

namespace SIENN.Services.Resources
{
    using System.Collections.ObjectModel;

    public class ProductResource
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime DeliveryDate { get; set; }
        public ICollection<int> Categories { get; set; }
        public int TypeId { get; set; }
        public int UnitId { get; set; }

        public ProductResource()
        {
            Categories = new Collection<int>();
        }
    }
}
