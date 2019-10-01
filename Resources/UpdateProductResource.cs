using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using SuperMarket.API.Domain.Models;

namespace SuperMarket.API.Resources
{
    public class UpdateProductResource
    {
        
        public string Name { get; set; }

        public short QuantityInPackage { get; set; }

        public EUnitOfMeasurement UnitOfMeasurement { get; set; }

        public int categoryId { get; set; }

    }
}
