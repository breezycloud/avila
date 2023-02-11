using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvilaNaturalle.Shared.Models
{
    public class Item
    {
        [Key] public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string? ItemName { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Piece is required!")]
        public int? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "Amount is required!")]
        public decimal? BuyPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "Amount is required!")]
        public decimal? SellPrice { get; set; }        
        public bool IsDeleted { get; set; } = false;
        public DateTime ModifiedTicks { get; set; } = DateTime.Now;
        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }
        public virtual List<OrderItem>? TransactionDetails { get; set; } = new();
        public virtual List<Stock>? Stocks { get; set; } = new();
    }
}
