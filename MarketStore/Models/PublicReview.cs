using System;
using System.Collections.Generic;

#nullable disable

namespace MarketStore.Models
{
    public partial class PublicReview
    {
        public decimal Id { get; set; }
        public string Masseage { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
