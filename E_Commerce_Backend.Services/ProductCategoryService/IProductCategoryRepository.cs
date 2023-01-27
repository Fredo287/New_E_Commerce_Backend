using E_Commerce_Backend.ErrorHandler;
using E_Commerce_Backend.Models;
using E_Commerce_Backend.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Backend.Services.ProductCategoryService
{
    public interface IProductCategoryRepository
    {
        public List<ProductCategory> AllProductCategories();
        public ProductCategory SingleProductCategory(int id);
        public ProductCategoryErrorHandler AddProductCategory(ProductCategoryRequest request);
        public ProductCategoryErrorHandler UpdateProductCategory(int id, ProductCategoryRequest request);
        public ProductCategoryErrorHandler DeleteProductCategory(int id);
    }
}
