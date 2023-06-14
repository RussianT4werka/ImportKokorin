using System;
using System.Collections.Generic;

namespace ImportKokorin.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int GenderId { get; set; }
        public string IpAdress { get; set; } = null!;

        public virtual Gender Gender { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
