using AutoFixture;
using E_Commerce_Backend.Controllers;
using E_Commerce_Backend.Models;
using E_Commerce_Backend.Requests;
using E_Commerce_Backend.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;

namespace E_Commerce_Backend.Tests
{
    public class ProductControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IProductRepository> _serviceMock;
        private readonly ProductController _sut;

        public ProductControllerTests()
        {
            _fixture= new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IProductRepository>>();
            _sut = new ProductController(_serviceMock.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(x => _fixture.Behaviors.Remove(x));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
        }

        //public ProductControllerTests()
        //{
        //    _fixture = new Fixture();
        //    _mock = _fixture.Freeze<Mock<IProductRepository>>();
        //    //_productController = new ProductController(_mock.Object);

        //    _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(x => _fixture.Behaviors.Remove(x));
        //    _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
        //}


        // Test case for GetProduct(int id)
        [Fact]
        public void GetProduct_ShouldReturnOkResponse_WhenProductForGivenIdIsFound()
        {
            // Arrange
            var id = 1;


            // Act
            var result = _sut.GetProduct(id);
            OkObjectResult okObjectResult = result as OkObjectResult;


            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.GetType().Should().Be(typeof(OkObjectResult));

            Assert.Equal(200, okObjectResult.StatusCode);
            //_serviceMock.Verify(x => x.SingleProduct(id), Times.Once);
        }



        // Test case for SearchProduct
        [Fact]
        public void SearchProduct_ShouldReturnOkResponse_WhenSoughtProductIsFound()
        {
            // Arrange
            var request = new SearchProductRequest { Name = "Gaming Laptop" };
            var resultsMock = _fixture.Create<List<Product>>();
            _serviceMock.Setup(x => x.SearchProduct(request)).Returns(resultsMock);


            // Act
            var result = _sut.SearchProduct(request);
            OkObjectResult okObjectResult = result as OkObjectResult;


            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.GetType().Should().Be(typeof(OkObjectResult));

            Assert.Equal(200, okObjectResult.StatusCode);
            _serviceMock.Verify(x => x.SearchProduct(request), Times.Once);
        }


        [Fact]
        public void SearchProduct_ShouldReturnBadRequestResponse_WhenSoughtProductIsNotFound()
        {
            //Arrange
            var request = new SearchProductRequest
            {
                Name = string.Empty,
            };


            List<Product> response = new List<Product>();

            _serviceMock.Setup(x => x.SearchProduct(request)).Returns(response);


            //Act
            var result = _sut.SearchProduct(request);
            BadRequestObjectResult badResult = result as BadRequestObjectResult;


            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();
            result.GetType().Should().Be(typeof(BadRequestObjectResult));
            Assert.Equal(404, badResult.StatusCode);

            _serviceMock.Verify(x => x.SearchProduct(request), Times.Once);
        }
    }
}