using System;
using System.Collections.Generic;

namespace ImportKokorin.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Count { get; set; }
        public int PhotoId { get; set; }

        public virtual Photo Photo { get; set; } = null!;
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
