using E_Commerce_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Backend.Models
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<Product> Products { get; set; } = new List<Product>();

    }
}
