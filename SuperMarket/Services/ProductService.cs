using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Repositories;
using SuperMarket.API.Domain.Services;
using SuperMarket.API.Domain.Services.Communication;

namespace SuperMarket.API.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _productRepository.ListAsync();
        }

        public async Task<ProductResponse> Get_Product_ByID(int id)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
                return new ProductResponse($"Product with {id.ToString("#0#")} not found!");

            return new ProductResponse(existingProduct);

        }

        public async Task<ProductResponse> SaveAsync(Product product)
        {
            try
            {
                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when saving the product: {ex.Message}");
            }

        }

        public async Task<ProductResponse> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
                return new ProductResponse($"Product with {id.ToString("#0#")} not found!");

            product.Id = existingProduct.Id;

            if (!checkProductDetails(ref existingProduct, ref product))
                return new ProductResponse("Same product details, No changes made!");
            
            try
            {
                _productRepository.Update(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                // Do Some logging stuff
                return new ProductResponse($"An Error occurred when updating the product: {ex.Message}");
            }
        }

        private bool checkProductDetails(ref Product existingProduct, ref Product updateProduct)
        {
            bool valuesChanged = false;
            
            if (existingProduct.Name != updateProduct.Name)
            {
                existingProduct.Name = updateProduct.Name;
                valuesChanged = true;
            }

            if (existingProduct.QuantityInPackage != updateProduct.QuantityInPackage)
            {
                existingProduct.QuantityInPackage = updateProduct.QuantityInPackage;
                valuesChanged = true;
            }

            if (existingProduct.UnitOfMeasurement != updateProduct.UnitOfMeasurement)
            {
                existingProduct.UnitOfMeasurement = updateProduct.UnitOfMeasurement;
                valuesChanged = true;
            }

            if (existingProduct.CategoryId != updateProduct.CategoryId)
            {
                existingProduct.CategoryId = updateProduct.CategoryId;
                valuesChanged = true;
            }
                
            return valuesChanged;

        }

        public async Task<ProductResponse> DeleteAsync(int id)
        {

            var existingProduct = await _productRepository.FindByIdAsync(id);

            if (existingProduct == null)
                return new ProductResponse($"Product with {id.ToString("#0#")} not found!");

            try
            {
                _productRepository.Remove(existingProduct);
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(existingProduct);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ProductResponse($"An error occurred when deleting product: {ex.Message}!");
            }
        }
    }
}
