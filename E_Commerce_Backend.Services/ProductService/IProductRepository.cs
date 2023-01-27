using E_Commerce_Backend.ErrorHandler;
using E_Commerce_Backend.Models;
using E_Commerce_Backend.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Backend.Services.ProductService
{
    public interface IProductRepository
    {
        public List<Product> AllProduct();
        public Product SingleProduct(int id);
        public ProductErrorHandler AddProduct(ProductRequest request);
        public ProductErrorHandler UpdateProduct(int id, ProductRequest request);
        public ProductErrorHandler DeleteProduct(int id);
        public List<Product> SearchProduct(SearchProductRequest request);
    }
}
