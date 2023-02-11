using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvilaNaturalle.Shared.Models
{
    public class DebtorDataModel : Order
    {
        public string? CustomerName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public new decimal TotalAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public new decimal Discount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public new decimal SubTotal { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public  decimal Paid { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public new decimal Balance { get; set; }
    }
}
