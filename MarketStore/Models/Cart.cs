using System;
using System.Collections.Generic;

#nullable disable

namespace MarketStore.Models
{
    public partial class Cart
    {
        public decimal Id { get; set; }
        public decimal? UserId { get; set; }
        public decimal? ProductId { get; set; }
        public byte? Quntity { get; set; }

        public virtual Product Product { get; set; }
        public virtual User1 User { get; set; }
    }
}
