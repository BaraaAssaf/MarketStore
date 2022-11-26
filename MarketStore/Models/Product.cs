using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MarketStore.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            Productorders = new HashSet<Productorder>();
        }

        public decimal Id { get; set; }
        public string ProductName { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string Details { get; set; }
        public int Quntity { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public decimal? StoreId { get; set; }

        public virtual Store Store { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Productorder> Productorders { get; set; }
    }
}
