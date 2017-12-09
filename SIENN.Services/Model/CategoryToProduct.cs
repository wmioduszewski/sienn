using System;
using System.Collections.Generic;
using System.Text;

namespace SIENN.Services.Model
{
    public class CategoryToProduct
    {
        //idea got from https://blog.oneunicorn.com/2017/09/25/many-to-many-relationships-in-ef-core-2-0-part-1-the-basics/
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
