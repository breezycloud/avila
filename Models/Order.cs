using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvilaNaturalle.Shared.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime TransactionDate { get; set; }
        public Guid CustomerId { get; set; }
        public string? ReceiptNo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount => OrderItems?.Sum(x => x.Quantity * x.Price) ?? 0;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SubTotal => TotalAmount - Discount;
        [NotMapped]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Pay { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance => SubTotal - PaymentDetails!.Sum(x => x.Amount) - Pay;
        public bool IsDeleted { get; set; } = false;
        public long ModifiedTicks { get; set; } = DateTime.Now.Ticks;
        public virtual List<OrderItem>? OrderItems { get; set; } = new();
        public virtual List<OrderPayment>? PaymentDetails { get; set; } = new();
        public virtual Customer? Customer { get; set; }
    }
}
