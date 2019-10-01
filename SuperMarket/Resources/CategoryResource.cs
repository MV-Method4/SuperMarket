using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarket.API.Resources
{
    /// <summary>
    /// Category Details
    /// </summary>
    public class CategoryResource
    {
        /// <summary>
        /// Id of category
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// name of category
        /// </summary>
        public string Name { get; set; }
    }
}
