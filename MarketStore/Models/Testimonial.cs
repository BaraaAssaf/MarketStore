using System;
using System.Collections.Generic;

#nullable disable

namespace MarketStore.Models
{
    public partial class Testimonial
    {
        public decimal Id { get; set; }
        public string Masseage { get; set; }
        public string StatusTestimonials { get; set; }
        public decimal? UserId { get; set; }

        public virtual User1 User { get; set; }
    }
}
