using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Backend.Models;

namespace E_Commerce_Backend.ErrorHandler
{
    public class UserErrorHandler
    {
        public bool State { get; set; }
        public string User { get; set; } = string.Empty;
        public User Object { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
