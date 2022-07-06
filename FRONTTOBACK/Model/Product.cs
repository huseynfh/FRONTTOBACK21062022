﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FRONTTOBACK.Model
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [NotMapped]
        public  IFormFile Photo { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int Count { get; set; }
    }
}
