using System;
using System.Collections.Generic;

namespace ImportKokorin.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int StatusId { get; set; }

        public virtual Status Status { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
