﻿using System.ComponentModel.DataAnnotations;

namespace PruebasUnitarias.Models
{
    public class Product
    {
        public int Id { get; set; } // ID único para cada producto
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }
}
    