using Azure.Core;
using E_Commerce_Backend.Requests;
using E_Commerce_Backend.Services.ProductCategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryRepository _productcategoriesServices;
        public ProductCategoryController(IProductCategoryRepository repository)
        {
            _productcategoriesServices = repository;
        }

        [HttpGet]
        public IActionResult GetAllProductCategories()
        {
            var myProductcategories = _productcategoriesServices.AllProductCategories();
            if (myProductcategories is null)
            {
                return NotFound();
            }
            return Ok(myProductcategories);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingleProductCategory(int id)
        {
            var myProductcategories = _productcategoriesServices.SingleProductCategory(id);
            if (myProductcategories is null)
            {
                return NotFound();
            }

            return Ok(myProductcategories);
        }

        [HttpPost("addProductCategory"), Authorize(Roles = "Admin")]
        public IActionResult AddProductCategory(ProductCategoryRequest request)
        {
            var response = _productcategoriesServices.AddProductCategory(request);

            if (response.State == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public IActionResult UpdateProductCategory(int id, ProductCategoryRequest request)
        {
            var response = _productcategoriesServices.UpdateProductCategory(id, request);


            if (response.State == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public IActionResult DeleteProductCategory(int id)
        {
            var response = _productcategoriesServices.DeleteProductCategory(id);


            if (response.State == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
