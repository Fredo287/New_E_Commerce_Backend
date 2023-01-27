using E_Commerce_Backend.DataAccess;
using E_Commerce_Backend.ErrorHandler;
using E_Commerce_Backend.Models;
using E_Commerce_Backend.Requests;
using E_Commerce_Backend.Services.ProductCategoryService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Backend.Services.ProductService
{
    public class ProductService : IProductRepository
    {
        private readonly IProductCategoryRepository _productCategoryService;
        private ProductErrorHandler response;
        private readonly DataContext _context = new DataContext();

        public ProductService(IProductCategoryRepository repository)
        {
            _productCategoryService = repository;
        }

        public ProductErrorHandler AddProduct(ProductRequest request)
        {
            if (_context.Products.Any(x => x.Name == request.Name))
            {
                response = SetResponse(false, "Product already exists!", null);
                return response;
            }
            var allProductCategories = _productCategoryService.AllProductCategories();
            if (!allProductCategories.Any(u => u.ProductCategoryId == request.CategoryId))
            {
                response = SetResponse(false, "Product category does not exist!", null);
                return response;
            }

            try
            {
                var Product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    Stock = request.Stock,
                    CategoryId = request.CategoryId
                };
                _context.Products.Add(Product);
                _context.SaveChangesAsync();
                response = SetResponse(true, "Product added successfully!", Product);
            }
            catch (Exception ex)
            {
                response = SetResponse(false, "Product could not be added to the system. Please try again!", null);

            }

            return response;
        }

        public List<Product> AllProduct()
        {
            return _context.Products.ToList();
        }

        public ProductErrorHandler DeleteProduct(int id)
        {
            var MyProductServices = AllProduct();
            if (!MyProductServices.Any(u => u.ProductId == id))
            {
                response = SetResponse(false, "Product does not exist!", null);
                return response;
            }

            var product = SingleProduct(id);

            _context.Products.Remove(product);
            _context.SaveChangesAsync();
            response = SetResponse(true, "Product deletion successful!", product);
            return response;
        }

        public List<Product> SearchProduct(SearchProductRequest request)
        {
            var CategoryId = _context.ProductCategories.Where(c => c.Name == request.Name).Select(c => c.ProductCategoryId).FirstOrDefault();

            if (CategoryId != 0)
            {
                return _context.Products.Where(p => p.CategoryId == CategoryId).ToList();
            }

            return _context.Products.Where(p => p.Name == request.Name).ToList();
        }

        public Product SingleProduct(int id)
        {
            return _context.Products.Find(id);
        }

        public ProductErrorHandler UpdateProduct(int id, ProductRequest request)
        {
            var myProduct = AllProduct();
            if (myProduct.Any(x => x.ProductId == id))
            {
                var AllProductcategories = _productCategoryService.AllProductCategories();
                if (!AllProductcategories.Any(x => x.ProductCategoryId == request.CategoryId))
                {
                    response = SetResponse(false, "Specified product category does not match!", null);
                    return response;
                }
                if (_context.Products.Any(u => u.Name == request.Name))
                {
                    response = SetResponse(false, "Product exists!", null);
                    return response;
                }
                var product = SingleProduct(id);

                product.Name = request.Name;
                product.Description = request.Description;
                product.Stock = request.Stock;
                product.CategoryId = request.CategoryId;
                _context.Products.Update(product);
                _context.SaveChangesAsync();
                response = SetResponse(true, "Product was successfully updated!", product);
                return response;
            }
            response = SetResponse(false, "Specified product does not exist!", null);
            return response;
        }

        private ProductErrorHandler SetResponse(bool state, string message, Product obj)
        {
            ProductErrorHandler response = new ProductErrorHandler
            {
                State = state,
                Object = obj,
                Message = message
            };

            return response;
        }
    }
}
