using System;
using System.Collections.Generic;

#nullable disable

namespace MarketStore.Models
{
    public partial class Productorder
    {
        public decimal Id { get; set; }
        public decimal? OrderId { get; set; }
        public decimal? ProductId { get; set; }
        public byte? Quntity { get; set; }

        public virtual Order2 Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
