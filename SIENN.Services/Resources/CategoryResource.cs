namespace SIENN.Services.Resources
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryResource
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Code { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
    }
}