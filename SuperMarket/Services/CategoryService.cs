using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Services;
using SuperMarket.API.Domain.Repositories;
using SuperMarket.API.Domain.Services.Communication;

namespace SuperMarket.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRespository _categoryRespository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRespository categoryRespository, IUnitOfWork unitOfWork)
        {
            _categoryRespository = categoryRespository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _categoryRespository.ListAsync();
        }

        public async Task<CategoryResponse> SaveAsync(Category category)
        {
            try
            {
                await _categoryRespository.AddAsync(category);
                await _unitOfWork.CompleteAsync();

                return new CategoryResponse(category);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CategoryResponse($"An error occurred when saving the category: {ex.Message}");
            }

        }

        public async Task<CategoryResponse> UpdateAsync(int id, Category category)
        {
            var existingCategory = await _categoryRespository.FindByIdAsync(id);

            if (existingCategory == null)
                return new CategoryResponse("Category not found!");

            existingCategory.Name = category.Name;

            try
            {
                _categoryRespository.Update(existingCategory);
                await _unitOfWork.CompleteAsync();

                return new CategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                // Do Some logging stuff
                return new CategoryResponse($"An Error occurred when updating the category: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> DeleteAsync(int id)
        {

            var existingCategory = await _categoryRespository.FindByIdAsync(id);

            if (existingCategory == null)
                return new CategoryResponse("Category not found!");

            try
            {
                _categoryRespository.Remove(existingCategory);
                await _unitOfWork.CompleteAsync();

                return new CategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CategoryResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }

    }
}
