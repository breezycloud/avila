using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvilaNaturalle.Shared.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [StringLength(11, ErrorMessage = "Phone No is should 10 characters", MinimumLength = 11)]
        public string? MobileNo1 { get; set; }
        [StringLength(11, ErrorMessage = "Phone No is should 10 characters", MinimumLength = 11)]
        public string? MobileNo2 { get; set; }
        [RegularExpression("^[A-Za-z0-9_\\+-]+(\\.[A-Za-z0-9_\\+-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*\\.([A-Za-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }
        public string? Username { get; set; }
        public int Bday { get; set; }
        public int Bmonth { get; set; }
        public string? Religion { get; set; }
        public string? CustomerName => $"{FirstName} {LastName}";
        public string? ContactAddress { get; set; }
        public virtual List<Order>? Orders { get; set; }
    }
}
