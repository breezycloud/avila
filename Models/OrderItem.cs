using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvilaNaturalle.Shared.Models
{
    public class OrderItem
    {
        [Key] public Guid Id { get; set; }
        public Guid TransactionId { get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsDeleted { get; set; } = false;
        public long ModifiedTicks { get; set; } = DateTime.Now.Ticks;
        [ForeignKey(nameof(TransactionId))]
        public virtual Order? Transaction { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual Item? Item {  get; set; }
    }
}
