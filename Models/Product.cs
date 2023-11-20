using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreDapper.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }
    }
}