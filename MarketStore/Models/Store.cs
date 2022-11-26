using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MarketStore.Models
{
    public partial class Store
    {
        public Store()
        {
            Order2s = new HashSet<Order2>();
            Products = new HashSet<Product>();
        }

        public decimal Id { get; set; }
        public string StoreName { get; set; }
        public decimal MonthlyFee { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public decimal? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Order2> Order2s { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
