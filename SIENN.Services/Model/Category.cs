

namespace SIENN.Services.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Code { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public ICollection<CategoryToProduct> Products { get; set; }

        public Category()
        {
            Products = new Collection<CategoryToProduct>();
        }
    }
}
