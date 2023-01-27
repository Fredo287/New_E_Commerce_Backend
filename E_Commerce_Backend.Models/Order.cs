using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce_Backend.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public int ReferenceId { get; set; }
        public string Email { get; set; } = string.Empty;
        //public User User { get; set; } = new User();
        [JsonIgnore]
        public List<Product> Products { get; set; } = new List<Product>();

    }
    public enum Status
    {
        Success,
        Processing
    }
}
