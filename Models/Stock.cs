using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvilaNaturalle.Shared.Models;

public class Stock
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required(ErrorMessage = "Date field is required")]
    public DateTime Date { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "Quantity is required")]
    public int? Quantity { get; set; } = 0;
    public Guid ProductID { get; set; }
    [NotMapped]
    public bool IsDeleted { get; set; } = false;
    public long ModifiedTicks { get; set; } = DateTime.Now.Ticks;
    [ForeignKey("ProductID")]
    public virtual Item? Product { get; set; }
}
