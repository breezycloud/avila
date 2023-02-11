using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvilaNaturalle.Shared.Models
{
    public class OrderPayment
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid TransactionId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public PaymentMode PaymentMode { get; set; } = PaymentMode.None;
        public bool IsDeleted { get; set; } = false;
        public long ModifiedTicks { get; set; } = DateTime.Now.Ticks;
        [ForeignKey(nameof(TransactionId))]
        public virtual Order? Transaction { get; set; }
    }
}
