using System;
using System.Collections.Generic;

#nullable disable

namespace MarketStore.Models
{
    public partial class VisaCard
    {
        public decimal Id { get; set; }
        public long CardNumber { get; set; }
        public byte SecurityCode { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal? Balance { get; set; }
        public decimal? UserId { get; set; }

        public virtual User1 User { get; set; }
    }
}
