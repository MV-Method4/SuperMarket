using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using SuperMarket.API.Domain.Models;

namespace SuperMarket.API.Resources
{
    public class SaveProductResource
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required] 
        [Range(0,100)]
        public short QuantityInPackage { get; set; }

        [Required]
        [Range(1,5)]
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }

        public int categoryId { get; set; }



    }
}
