using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIENN.WebApi.Model
{
    public class Unit
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
