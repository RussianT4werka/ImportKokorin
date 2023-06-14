using System;
using System.Collections.Generic;

namespace ImportKokorin.Models
{
    public partial class Photo
    {
        public Photo()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public byte[] FileName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
