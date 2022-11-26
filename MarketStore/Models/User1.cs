using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MarketStore.Models
{
    public partial class User1
    {
        public User1()
        {
            Carts = new HashSet<Cart>();
            Order2s = new HashSet<Order2>();
            Testimonials = new HashSet<Testimonial>();
            VisaCards = new HashSet<VisaCard>();
        }

        public decimal Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order2> Order2s { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
        public virtual ICollection<VisaCard> VisaCards { get; set; }
    }
}
