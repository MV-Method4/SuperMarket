using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SuperMarket.API.Domain.Models;

namespace SuperMarket.API.Domain.Services.Communication
{
    public class ProductResponse : BaseResponse
    {
        public Product Product { get; protected set; }

        private ProductResponse(bool success, string message, Product product) : base(success, message)
        {
            Product = product;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="product">Saved product</param>
        /// <return>Response.</return>
        public ProductResponse(Product product) : this(true, string.Empty, product)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <return>Response.</return>
        public ProductResponse(string message) : this(false, message, null)
        { }
    }
}
