using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarket.API.Resources
{
    
    public class ProductResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short QuantityInPackage { get; set; }
        public string UnitOfMeasurement { get; set; }
        public CategoryResource Category { get; set; }

        public decimal Price { get; set; }
        public string ImageThumbUrl { get; set; }
        public string ImageUrl { get; set; }
        public bool IsProductOfTheWeek { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
    }
}
