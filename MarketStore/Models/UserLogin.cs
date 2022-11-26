using System;
using System.Collections.Generic;

#nullable disable

namespace MarketStore.Models
{
    public partial class UserLogin
    {
        public decimal Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal? RoleloginId { get; set; }

        public virtual RoleLogin Rolelogin { get; set; }
    }
}
