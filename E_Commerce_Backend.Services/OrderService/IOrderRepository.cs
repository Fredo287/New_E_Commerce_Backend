using E_Commerce_Backend.ErrorHandler;
using E_Commerce_Backend.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Backend.Services.OrderService
{
    public interface IOrderRepository
    {
        public OrderErrorHandler CreateOrder(int id, OrderRequest request);
    }
}
