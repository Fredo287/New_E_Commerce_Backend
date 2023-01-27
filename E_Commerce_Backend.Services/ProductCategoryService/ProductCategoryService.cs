using E_Commerce_Backend.DataAccess;
using E_Commerce_Backend.ErrorHandler;
using E_Commerce_Backend.Models;
using E_Commerce_Backend.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Backend.Services.ProductCategoryService
{
    public class ProductCategoryService : IProductCategoryRepository
    {

        private readonly DataContext _context = new DataContext();
        private ProductCategoryErrorHandler response;


        public List<ProductCategory> AllProductCategories()
        {
            //var ProductCategories = new List<ProductCategory>();
            //var ProductCategory1 = new ProductCategory
            //{
            //    ProductCategoryId = 1,
            //    Name = "Electronics",
            //    Description = "Typical electronic devices."
            //};
            //ProductCategories.Add(ProductCategory1);


            //var ProductCategory2 = new ProductCategory
            //{
            //    ProductCategoryId = 2,
            //    Name = "Electrical",
            //    Description = "Typical electrical equipment."
            //};
            //ProductCategories.Add(ProductCategory2);


            //var ProductCategory3 = new ProductCategory
            //{
            //    ProductCategoryId = 3,
            //    Name = "Clothes",
            //    Description = "Variety of clothes."
            //};

            //ProductCategories.Add(ProductCategory3);
            //return ProductCategories;

            return _context.ProductCategories.ToList();

        }

        public ProductCategoryErrorHandler DeleteProductCategory(int id)
        {
            var allProductCategories = AllProductCategories();
            if (!allProductCategories.Any(u => u.ProductCategoryId == id))
            {
                response = SetResponse(false, "Product category does not exist!", null);
                return response;
            }

            var selectedProductCategory = SingleProductCategory(id);

            _context.ProductCategories.Remove(selectedProductCategory);
            _context.SaveChangesAsync();
            response = SetResponse(true, "Product category deletion successful!", selectedProductCategory);
            return response;
        }

        public ProductCategory SingleProductCategory(int id)
        {
            return _context.ProductCategories.Find(id);
        }

        public ProductCategoryErrorHandler UpdateProductCategory(int id, ProductCategoryRequest request)
        {
            var allProductCategories = AllProductCategories();

            if (!allProductCategories.Any(x => x.ProductCategoryId == id))
            {
                response = SetResponse(false, "Product catergory is not in the system", null);
                return response;
            }

            var SelectedProductCatergory = SingleProductCategory(id);

            SelectedProductCatergory.Name = request.Name;
            SelectedProductCatergory.Description = request.Description;

            _context.ProductCategories.Update(SelectedProductCatergory);
            _context.SaveChangesAsync();
            response = SetResponse(true, "Updated product catergory", SelectedProductCatergory);
            return response;
        }


        public ProductCategoryErrorHandler AddProductCategory(ProductCategoryRequest request)
        {
            var ProductCategory = new ProductCategory
            {
                Name = request.Name,
                Description = request.Description,
            };

            _context.ProductCategories.Add(ProductCategory);
            _context.SaveChangesAsync();
            response = SetResponse(true, "Product category was added successfully!", ProductCategory);
            return response;
        }


        private ProductCategoryErrorHandler SetResponse(bool state, string message, ProductCategory obj)
        {
            ProductCategoryErrorHandler response = new ProductCategoryErrorHandler
            {
                State = state,
                Message = message,
                Object = obj,
            };

            return response;
        }
    }
}
