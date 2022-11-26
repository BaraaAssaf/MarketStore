using System;
using System.Collections.Generic;

#nullable disable

namespace MarketStore.Models
{
    public partial class Contactu
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string MapLink { get; set; }
        public string Location { get; set; }
    }
}
