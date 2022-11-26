using System;
using System.Collections.Generic;

#nullable disable

namespace MarketStore.Models
{
    public partial class RoleLogin
    {
        public RoleLogin()
        {
            UserLogins = new HashSet<UserLogin>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserLogin> UserLogins { get; set; }
    }
}
