using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Xunit;
using Moq;

using SuperMarket.API.Domain.Models;
using SuperMarket.API.Controllers;
using SuperMarket.API.Resources;
using SuperMarket.API.Domain.Services;
using AutoMapper;
using System.Threading.Tasks;
using SuperMarket.API.Mapping;
using SuperMarket.API.Domain.Services.Communication;

namespace SuperMarket.Tests
{
    public class SuperMarketAPI_ProductShould
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

        #region Invalid_Ojbects

        private SaveProductResource OneProductResourceInvalid()
        {
            return new SaveProductResource()
            {
                Name = "beans",
                categoryId = 100,
                QuantityInPackage = 200,
                UnitOfMeasurement = EUnitOfMeasurement.Kilogram
            };
        }
        private Product OneProductInvalid()
        {
            return new Product()
            {
                Id = 100,
                Name = "Orange"

            };
        }

        private SaveProductResource OneSaveProductInvalid()
        {
            return new SaveProductResource()
            {
                Name = "Orange"

            };
        }

        #endregion

        #region Valid_Objects
        private SaveProductResource OneSaveProductResource()
        {
            return new SaveProductResource()
            {

                Name = "Orange",
                categoryId = 0,
                QuantityInPackage = 20,
                UnitOfMeasurement = EUnitOfMeasurement.Gram

            };
        }

        private Product OneProduct()
        {
            return new Product
            {
                Category = null,
                CategoryId = 0,
                Id = 101,
                Name = "Orange",
                QuantityInPackage = 20,
                UnitOfMeasurement = EUnitOfMeasurement.Gram
            };
        }

        private List<Product> Multiple_Products()
        {
            return new List<Product>()
            {
                new Product
                {
                    Name= "Oranges",
                    Category = null,
                    CategoryId = 0,
                    Id = 100,
                    QuantityInPackage = 5,
                    UnitOfMeasurement = EUnitOfMeasurement.Gram
                },
                new Product
                {
                    Name= "Bananas",
                    Category = null,
                    CategoryId = 0,
                    Id = 101,
                    QuantityInPackage = 7,
                    UnitOfMeasurement = EUnitOfMeasurement.Gram
                },
                new Product
                {
                    Name= "Milk",
                    Category = null,
                    CategoryId = 0,
                    Id = 102,
                    QuantityInPackage = 1,
                    UnitOfMeasurement = EUnitOfMeasurement.Liter
                }
            };
        }

        #endregion

        #region GET_EndPoints

        [Fact]
        public async Task GET_OKResult_ReturnedProductList()
        {
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.ListAsync()).ReturnsAsync(new List<Product>());

            var sut_productController = new ProductsController(productService.Object, BuildMapper());

            var result = await sut_productController.GetAllAsync() as List<ProductResource>;
            Assert.IsType<List<ProductResource>>(result);
        }

        [Fact]
        public async Task GET_OKResult_MultipleProducts()
        {
            var multiProducts = Multiple_Products();

            var productService = new Mock<IProductService>();
            productService.Setup(x => x.ListAsync()).ReturnsAsync(multiProducts);

            var sut_productController = new ProductsController(productService.Object, BuildMapper());

            var result = (List<ProductResource>)await sut_productController.GetAllAsync();

            Assert.True(result != null && result.Count == multiProducts.Count);

        }

        #endregion

        #region POST_EndPoint

        [Fact]
        public async Task POST_BadResult()
        {

            var productService = new Mock<IProductService>();
            productService.Setup(x => x.SaveAsync(It.IsAny<Product>())).ReturnsAsync(
                new ProductResponse("Error")

                );

            var sut_productController = new ProductsController(productService.Object, BuildMapper());

            var result = await sut_productController.PostAsync(null) as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task POST_BadResult_Invalid_ProductModelState()
        {

            var oneProduct = OneProduct();
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.SaveAsync(It.IsAny<Product>())).ReturnsAsync(
                new ProductResponse(oneProduct)
                );

            var sut_productController = new ProductsController(productService.Object, BuildMapper());
            string errMsg = "Missing Product Name!";
            sut_productController.ModelState.AddModelError("Invalid", errMsg);
            var result = await sut_productController.PostAsync(OneSaveProductResource()) as BadRequestObjectResult;

            var strMsg = result.Value as List<string>;

            Assert.Equal(errMsg, strMsg[0]);

        }

        [Fact]
        public async Task POST_BadResult_InvalidProductErrorMsg()
        {
            var productResponse = new ProductResponse("Error");

            var productService = new Mock<IProductService>();
            productService.Setup(x => x.SaveAsync(It.IsAny<Product>())).ReturnsAsync(
                productResponse
                );

            var sut_productController = new ProductsController(productService.Object, BuildMapper());

            var result = await sut_productController.PostAsync(null) as BadRequestObjectResult;

            Assert.Equal(productResponse.Message, result.Value);
        }

        [Fact]
        public async Task POST_OKResult()
        {

            var oneProduct = OneProduct();
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.SaveAsync(It.IsAny<Product>())).ReturnsAsync(
                new ProductResponse(oneProduct)
                );

            var sut_productController = new ProductsController(productService.Object, BuildMapper());
            
            var result = await sut_productController.PostAsync(OneSaveProductResource()) as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);


        }

        [Fact]
        public async Task POST_OKResult_ProductMatch()
        {
            var oneSaveProductResource = OneSaveProductResource();
            var prodcutResponse = new ProductResponse(OneProduct());

            var productService = new Mock<IProductService>();
            productService.Setup(x => x.SaveAsync(It.IsAny<Product>())).ReturnsAsync(
                prodcutResponse
                );
            
            var sut_productController = new ProductsController(productService.Object, BuildMapper());

            var result = await sut_productController.PostAsync(oneSaveProductResource) as OkObjectResult;

            var resultPR = (ProductResource)result.Value;
            Assert.True(
                oneSaveProductResource.Name == resultPR.Name &&
                oneSaveProductResource.QuantityInPackage == resultPR.QuantityInPackage
                );

        }

        #endregion

        #region PUT_EndPoints

        [Fact]
        public async Task PUT_OKResult()
        {
            var product = OneProduct();
            var productResponse = new ProductResponse(product);
            var saveProduct = OneSaveProductResource();
            
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.UpdateAsync(product.Id, It.IsAny<Product>())).ReturnsAsync(
                productResponse
                );
            
            var sut_productController = new ProductsController(productService.Object, BuildMapper());

            var result = await sut_productController.PutAsync(product.Id, saveProduct) as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task PUT_BadResult_InvalidModelState()
        {
            
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.UpdateAsync(101, It.IsAny<Product>())).ReturnsAsync(
                new ProductResponse("Error with ID")
                );

            var sut_productController = new ProductsController(productService.Object, BuildMapper());
            sut_productController.ModelState.AddModelError("Invalid ID", "Error with ID");

            var result = await sut_productController.PutAsync(101, It.IsAny<SaveProductResource>()) as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task PUT_BadResult_InvalidModelState_ErrorMsgMatch()
        {
            string actualErrMsg = "Error with ID";
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.UpdateAsync(101, It.IsAny<Product>())).ReturnsAsync(
                new ProductResponse(actualErrMsg)
                );

            var sut_productController = new ProductsController(productService.Object, BuildMapper());
            sut_productController.ModelState.AddModelError("Invalid ID", actualErrMsg);

            var result = await sut_productController.PutAsync(101, It.IsAny<SaveProductResource>()) as BadRequestObjectResult;

            var errMsg = result.Value as List<string>;
            Assert.Equal(actualErrMsg, errMsg[0]);

        }

        [Fact]
        public async Task PUT_BadResult_ErrorMsgMatch()
        {
            string actualErrMsg = "Error with ID";
            var productService = new Mock<IProductService>();
            productService.Setup(x => x.UpdateAsync(101, It.IsAny<Product>())).ReturnsAsync(
                new ProductResponse(actualErrMsg)
                );

            var sut_productController = new ProductsController(productService.Object, BuildMapper());
            
            var result = await sut_productController.PutAsync(101, It.IsAny<SaveProductResource>()) as BadRequestObjectResult;

            Assert.Equal(actualErrMsg, result.Value);

        }

        #endregion

        #region DELETE_EndPoint

        [Fact]
        public async Task DELETE_OKResult()
        {
            var product = OneProduct();
            var productResponse = new ProductResponse(product);
            var saveProduct = OneSaveProductResource();

            var productService = new Mock<IProductService>();
            productService.Setup(x => x.DeleteAsync(product.Id)).ReturnsAsync(
                productResponse
                );

            var sut_productController = new ProductsController(productService.Object, BuildMapper());

            var result = await sut_productController.DeleteAsync(product.Id) as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task DELETE_OKResult_ProductDeleted()
        {
            var product = OneProduct();
            var productResponse = new ProductResponse(product);
            var saveProduct = OneSaveProductResource();

            var productService = new Mock<IProductService>();
            productService.Setup(x => x.DeleteAsync(product.Id)).ReturnsAsync(
                productResponse
                );

            var sut_productController = new ProductsController(productService.Object, BuildMapper());

            var result = await sut_productController.DeleteAsync(product.Id) as OkObjectResult;

            var productDeleted = result.Value as ProductResource;

            Assert.Equal(product.Id, productDeleted.Id);

        }

        [Fact]
        public async Task DELETE_BadResult()
        {
            var product = OneProduct();
            var productResponse = new ProductResponse("Invalid Product ID");
            var saveProduct = OneSaveProductResource();

            var productService = new Mock<IProductService>();
            productService.Setup(x => x.DeleteAsync(product.Id)).ReturnsAsync(
                productResponse
                );


            var sut_productController = new ProductsController(productService.Object, BuildMapper());

            var result = await sut_productController.DeleteAsync(product.Id) as BadRequestObjectResult;

            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public async Task DELETE_BadResult_ErrorMsgMatch()
        {
            var product = OneProduct();
            var productResponse = new ProductResponse("Invalid Product ID");
            var saveProduct = OneSaveProductResource();

            var productService = new Mock<IProductService>();
            productService.Setup(x => x.DeleteAsync(product.Id)).ReturnsAsync(
                productResponse
                );

            var sut_productController = new ProductsController(productService.Object, BuildMapper());

            var result = await sut_productController.DeleteAsync(product.Id) as BadRequestObjectResult;

            Assert.Equal(productResponse.Message, result.Value);
        }

        #endregion






    }
}
