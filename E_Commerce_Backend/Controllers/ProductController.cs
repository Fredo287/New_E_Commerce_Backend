using Azure.Core;
using E_Commerce_Backend.Requests;
using E_Commerce_Backend.Services.ProductCategoryService;
using E_Commerce_Backend.Services.ProductService;
using E_Commerce_Backend.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace E_Commerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productService;

        public ProductController(IProductRepository repository)
        {
            _productService = repository;
        }

        

        [HttpGet]
        public IActionResult GetProducts()
        {
            var allProducts = _productService.AllProduct();
            if (allProducts is null)
            {
                return NotFound();
            }
            return Ok(allProducts);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var selectedProduct = _productService.SingleProduct(id);
            if (selectedProduct is null)
            {
                return NotFound();
            }

            return Ok(selectedProduct);
        }


        [HttpPost("AddProduct"), Authorize(Roles = "Admin")]
        public IActionResult AddProduct(ProductRequest request)
        {
            var response = _productService.AddProduct(request);

            if (response.State == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPut("{id}"), Authorize]
        public IActionResult UpdateProduct(int id, ProductRequest request)
        {
            var response = _productService.UpdateProduct(id, request);
            if (response.State == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }



        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public IActionResult DeleteProduct(int id)
        {
            var response = _productService.DeleteProduct(id);
            if (response.State == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("SearchForProduct"), Authorize]
        public IActionResult SearchProduct(SearchProductRequest request)
        {
            var response = _productService.SearchProduct(request);


            if(response.Count == 0)
            {
                return NotFound("Product not found!");
            }

            return Ok(response);
        }
    }
}
