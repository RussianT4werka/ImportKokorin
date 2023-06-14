using System;
using System.Collections.Generic;

namespace ImportKokorin.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
