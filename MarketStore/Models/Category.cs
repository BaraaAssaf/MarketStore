using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MarketStore.Models
{
    public partial class Category
    {
        public Category()
        {
            Stores = new HashSet<Store>();
        }

        public decimal Id { get; set; }
        public string CategoryName { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
