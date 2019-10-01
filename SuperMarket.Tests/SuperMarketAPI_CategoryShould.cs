using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Xunit;
using Moq;

using AutoMapper;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Mapping;
using SuperMarket.API.Resources;
using System.Threading.Tasks;
using SuperMarket.API.Domain.Services;
using SuperMarket.API.Controllers;
using SuperMarket.API.Domain.Services.Communication;

namespace SuperMarket.Tests
{
    public class SuperMarketAPI_CategoryShould
    {

        private IMapper BuildMapper()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(
                new List<Profile>
                {
                    new ModelToResourceProfile(),
                    new ResourceToModelProfile()
                }));
            var mapper = new Mapper(configuration);
            return mapper;
        }

        #region Mock_Items

        #region Valid_CategoryItems

        private Category Get_CategoryItem()
        {
            return new Category
            {
                Id = 101,
                Name= "Fruits & Vegs"
            };
        }

        private List<Category> Get_MultipleCategoryItems()
        {
            return new List<Category>()
            {
                new Category{
                    Id=101,
                    Name = "Fruits & Vegs"
                },
                new Category{
                    Id=102,
                    Name = "Dairy"
                }
            };
        }

        #endregion

        #region Invalid_CategoryItems

        #endregion
        
        #endregion


        #region GET_EndPoints

        [Fact]
        public async Task GET_OKResult_ReturnedCategoryList()
        {
            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.ListAsync()).ReturnsAsync(new List<Category>());

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());

            var result = await sut_categoryController.GetAllAsync() as List<CategoryResource>;
            Assert.IsType<List<CategoryResource>>(result);
        }

        [Fact]
        public async Task GET_OKResult_ReturnedMultipleCategoryList()
        {
            var multipleCategories = Get_MultipleCategoryItems();

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.ListAsync()).ReturnsAsync(multipleCategories);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());

            var result = await sut_categoryController.GetAllAsync() as List<CategoryResource>;
            Assert.True(result != null && multipleCategories.Count == result.Count);
        }

        #endregion

        #region POST_EndPoints

        [Fact]
        public async Task POST_OKResult()
        {
            var categoryResponse = new CategoryResponse(new Category());

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.SaveAsync(It.IsAny<Category>())).ReturnsAsync(categoryResponse);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());

            var result = await sut_categoryController.PostAsync(It.IsAny<SaveCategoryResource>()) as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task POST_OKResult_ValidObject()
        {
            var category = Get_CategoryItem();
            var categoryResponse = new CategoryResponse(category);

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.SaveAsync(It.IsAny<Category>())).ReturnsAsync(categoryResponse);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());

            var result = await sut_categoryController.PostAsync(It.IsAny<SaveCategoryResource>()) as OkObjectResult;
            var categoryResult = result.Value as CategoryResource;
            Assert.Equal(category.Id, categoryResult.Id);
        }

        [Fact]
        public async Task POST_BadResult_InvalidModelState()
        {
            var category = Get_CategoryItem();
            var categoryResponse = new CategoryResponse(category);

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.SaveAsync(It.IsAny<Category>())).ReturnsAsync(categoryResponse);
            
            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());
            string errMsg = "Invalid Category ID!";
            sut_categoryController.ModelState.AddModelError("Invalid", errMsg);


            var result = await sut_categoryController.PostAsync(It.IsAny<SaveCategoryResource>()) as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task POST_BadResult_InvalidModelState_ErrorMsgMatch()
        {
            var category = Get_CategoryItem();
            var categoryResponse = new CategoryResponse(category);

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.SaveAsync(It.IsAny<Category>())).ReturnsAsync(categoryResponse);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());
            string errMsg = "Invalid Category ID!";
            sut_categoryController.ModelState.AddModelError("Invalid", errMsg);


            var result = await sut_categoryController.PostAsync(It.IsAny<SaveCategoryResource>()) as BadRequestObjectResult;
            var resultMsg = result.Value as List<string>;

            Assert.Equal(errMsg, resultMsg[0]);
        }

        [Fact]
        public async Task POST_BadResult()
        {
            var category = Get_CategoryItem();
            string errMsg = "Invalid Category ID!";
            var categoryResponse = new CategoryResponse(errMsg);

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.SaveAsync(It.IsAny<Category>())).ReturnsAsync(categoryResponse);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());
            
            
            var result = await sut_categoryController.PostAsync(It.IsAny<SaveCategoryResource>()) as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task POST_BadResult_ErrorMsgMatch()
        {
            var category = Get_CategoryItem();
            string errMsg = "Invalid Category ID!";
            var categoryResponse = new CategoryResponse(errMsg);

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.SaveAsync(It.IsAny<Category>())).ReturnsAsync(categoryResponse);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());

            var result = await sut_categoryController.PostAsync(It.IsAny<SaveCategoryResource>()) as BadRequestObjectResult;
            Assert.Equal(errMsg, result.Value);
        }

        #endregion

        #region PUT_EndPoints

        [Fact]
        public async Task PUT_OKResult()
        {
            var categoryResponse = new CategoryResponse(new Category());

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.UpdateAsync(101, It.IsAny<Category>())).ReturnsAsync(categoryResponse);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());

            var result = await sut_categoryController.PutAsync(101, It.IsAny<SaveCategoryResource>()) as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PUT_OKResult_ValidObject()
        {
            var category = Get_CategoryItem();
            var categoryResponse = new CategoryResponse(category);

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.UpdateAsync(category.Id, It.IsAny<Category>())).ReturnsAsync(categoryResponse);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());

            var result = await sut_categoryController.PutAsync(category.Id, It.IsAny<SaveCategoryResource>()) as OkObjectResult;
            var categoryResult = result.Value as CategoryResource;
            Assert.Equal(category.Id, categoryResult.Id);
        }

        [Fact]
        public async Task PUT_BadResult_InvalidModelState()
        {
            var category = Get_CategoryItem();
            var categoryResponse = new CategoryResponse(category);

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.UpdateAsync(category.Id, It.IsAny<Category>())).ReturnsAsync(categoryResponse);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());
            string errMsg = "Invalid Category ID!";
            sut_categoryController.ModelState.AddModelError("Invalid", errMsg);


            var result = await sut_categoryController.PutAsync(category.Id, It.IsAny<SaveCategoryResource>()) as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PUT_BadResult_InvalidModelState_ErrorMsgMatch()
        {
            var category = Get_CategoryItem();
            var categoryResponse = new CategoryResponse(category);

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.UpdateAsync(category.Id, It.IsAny<Category>())).ReturnsAsync(categoryResponse);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());
            string errMsg = "Invalid Category ID!";
            sut_categoryController.ModelState.AddModelError("Invalid", errMsg);


            var result = await sut_categoryController.PutAsync(category.Id, It.IsAny<SaveCategoryResource>()) as BadRequestObjectResult;
            var resultMsg = result.Value as List<string>;

            Assert.Equal(errMsg, resultMsg[0]);
        }

        [Fact]
        public async Task PUT_BadResult()
        {
            var category = Get_CategoryItem();
            string errMsg = "Invalid Category ID!";
            var categoryResponse = new CategoryResponse(errMsg);

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.UpdateAsync(category.Id, It.IsAny<Category>())).ReturnsAsync(categoryResponse);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());

            var result = await sut_categoryController.PutAsync(category.Id, It.IsAny<SaveCategoryResource>()) as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PUT_BadResult_ErrorMsgMatch()
        {
            var category = Get_CategoryItem();
            string errMsg = "Invalid Category ID!";
            var categoryResponse = new CategoryResponse(errMsg);

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.UpdateAsync(category.Id, It.IsAny<Category>())).ReturnsAsync(categoryResponse);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());

            var result = await sut_categoryController.PutAsync(category.Id, It.IsAny<SaveCategoryResource>()) as BadRequestObjectResult;
            Assert.Equal(errMsg, result.Value);
        }

        #endregion

        #region DELETE_EndPoint

        [Fact]
        public async Task DELETE_OKResult()
        {
            var category = Get_CategoryItem();
            var categoryResponse = new CategoryResponse(category);

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.DeleteAsync(category.Id)).ReturnsAsync(categoryResponse);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());

            var result = await sut_categoryController.DeleteAsync(category.Id) as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DELETE_OKResult_CategoryMatch()
        {
            var category = Get_CategoryItem();
            var categoryResponse = new CategoryResponse(category);

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.DeleteAsync(category.Id)).ReturnsAsync(categoryResponse);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());

            var result = await sut_categoryController.DeleteAsync(category.Id) as OkObjectResult;
            var categoryResult = result.Value as CategoryResource;
            Assert.Equal(category.Id, categoryResult.Id);
        }

        [Fact]
        public async Task DELETE_BadResult()
        {
            var category = Get_CategoryItem();
            string errMsg = "Invalid Category ID!";
            var categoryResponse = new CategoryResponse(errMsg);

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.DeleteAsync(category.Id)).ReturnsAsync(categoryResponse);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());

            var result = await sut_categoryController.DeleteAsync(category.Id) as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DELETE_BadResult_ErrorMsgMatch()
        {
            var category = Get_CategoryItem();
            string errMsg = "Invalid Category ID!";
            var categoryResponse = new CategoryResponse(errMsg);

            var categoryService = new Mock<ICategoryService>();
            categoryService.Setup(x => x.DeleteAsync(category.Id)).ReturnsAsync(categoryResponse);

            var sut_categoryController = new CategoriesController(categoryService.Object, BuildMapper());

            var result = await sut_categoryController.DeleteAsync(category.Id) as BadRequestObjectResult;
            Assert.Equal(errMsg, result.Value);
        }

        #endregion


    }
}
