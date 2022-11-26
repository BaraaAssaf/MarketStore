using System;
using System.Collections.Generic;

#nullable disable

namespace MarketStore.Models
{
    public partial class Order2
    {
        public Order2()
        {
            Productorders = new HashSet<Productorder>();
        }

        public decimal Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal? UserId { get; set; }
        public decimal? StoreId { get; set; }
        public decimal? Totalprice { get; set; }
        public string LocationDelivery { get; set; }
        public string Status { get; set; }

        public virtual Store Store { get; set; }
        public virtual User1 User { get; set; }
        public virtual ICollection<Productorder> Productorders { get; set; }
    }
}
