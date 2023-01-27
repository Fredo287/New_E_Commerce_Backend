using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Backend.Requests
{
    public class ProductRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public int Stock { get; set; }
        public int CategoryId { get; set; }
    }
}
