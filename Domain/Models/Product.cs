using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarket.API.Domain.Models
{
    /// <summary>
    /// Product Resource Object
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Id of Product
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of Product
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Quantity In Package
        /// </summary>
        public short QuantityInPackage { get; set; }
        /// <summary>
        /// Unit of Measure  
        /// Range from 1 to 5  
        ///     1 = UN  
        ///     2 = MG  
        ///     3 = G  
        ///     4 = KG  
        ///     5 = L  
        /// </summary>
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }
        /// <summary>
        /// Id of category the product is in
        /// </summary>
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
