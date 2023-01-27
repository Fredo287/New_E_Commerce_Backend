using E_Commerce_Backend.DataAccess;
using E_Commerce_Backend.ErrorHandler;
using E_Commerce_Backend.Requests;
using E_Commerce_Backend.Services.ProductService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Backend.Services.OrderService
{
    public class OrderService : IOrderRepository
    {
        private OrderErrorHandler response;
        private readonly DataContext _context = new DataContext();
        private readonly IProductRepository _productService;

        public OrderService(IProductRepository productService)
        {
            _productService = productService;
        }

        public OrderErrorHandler CreateOrder(int productId, OrderRequest request)
        {
            if (!_context.Users.Any(o => o.Email == request.Email))
            {
                response = SetResponse(false, "User does not exist!", 404);
                return response;
            }

            var allProducts = _productService.AllProduct();
            if (!allProducts.Any(o => o.ProductId == productId))
            {
                response = SetResponse(false, "Product does not exist!", 404);
                return response;
            }

            var product = _productService.SingleProduct(productId);
            if(product.Stock < request.ProductQuantity)
            {
                response = SetResponse(false, "Not enough items in stock!", 404);
                return response;
            }

            var date = DateTime.Now;

            var order = new Models.Order
            {
                Date = date,
                Status = 0,
                ReferenceId = 777,
                Email = request.Email,
            };

            _context.Orders.Add(order);


            var newStock = product.Stock - request.ProductQuantity;

            var updatedProduct = new ProductRequest
            {
                Name = product.Name,
                Description = product.Description,
                Stock = newStock,
                CategoryId = product.CategoryId,
            };

            _productService.UpdateProduct(productId, updatedProduct);

            _context.SaveChangesAsync();
            Thread.Sleep(5000);

            response = SetResponse(true, "Order created successfully!", 777);
            return response;
        }


        private OrderErrorHandler SetResponse(bool state, string message, int tranactionId)
        {
            OrderErrorHandler response = new OrderErrorHandler
            {
                Status = state,
                Message = message,
                ReferenceId = tranactionId,
            };

            return response;
        }
    }
}
