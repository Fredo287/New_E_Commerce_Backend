using E_Commerce_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Backend.ErrorHandler
{
    public class ProductErrorHandler
    {
        public bool State { get; set; }

        public Product Object { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
