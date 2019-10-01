using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Services.Communication;

namespace SuperMarket.API.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<ProductResponse> SaveAsync(Product product);
        Task<ProductResponse> UpdateAsync(int id, Product product);
        Task<ProductResponse> DeleteAsync(int id);
    }
}
