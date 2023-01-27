using Azure;
using E_Commerce_Backend.DataAccess;
using E_Commerce_Backend.ErrorHandler;
using E_Commerce_Backend.Models;
using E_Commerce_Backend.Requests;
using E_Commerce_Backend.Services.OrderService;
using E_Commerce_Backend.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _dataContext = new DataContext();
        private readonly IOrderRepository _orderService;
        private OrderErrorHandler _errorHandler;

        public OrderController(IOrderRepository orderServices)
        {
            _orderService = orderServices;
        }

        [HttpPost("CreateOrder/{productId}"), Authorize(Roles = "Customer")]
        public async Task<ActionResult<Product>> CreateOrder(int productId, OrderRequest request)
        {
            var response = _orderService.CreateOrder(productId, request);
            if (response.Status == false)
            {
                return BadRequest(response);
            }

            var Id = 777;
            //var Id = response.ReferenceId;

            var product = await _dataContext.Products.Where(p => p.ProductId == productId).Include(r => r.Orders).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }

            var order = await _dataContext.Orders.Where(o => o.ReferenceId == Id).FirstOrDefaultAsync();
            if (order == null)
            {
                return NotFound();
            }

            //product.ProductOrders = order.ProductOrders;
            product.Orders.Add(order);
            await _dataContext.SaveChangesAsync();
            return Ok("Order created successfully!");
        }
    }
}
