using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TicketApp.Models
{
    public class Ticket
    {
       
            public int TicketID { get; set; }

            [Required(ErrorMessage = "Please enter a Name.")]
            [StringLength(30)]    
            public string Name { get; set; } = string.Empty;

            [Required(ErrorMessage = "Please enter a Description.")]
            [StringLength(50)]
            public string Description { get; set; } = string.Empty;

            [Required(ErrorMessage = "Please enter a Sprint Number.")]
            [Range(1, 10)]
            public string SprintNumber { get; set; } = string.Empty;

            [Required(ErrorMessage = "Please select a Point Value.")]
            [RegularExpression("[5,10,15,20]", ErrorMessage = "Must be 5, 10, 15, or 20")]
            public string PointValue { get; set; } = string.Empty;

            [Required(ErrorMessage = "Please select a Status.")]
            public string StatusID { get; set; } = string.Empty;

            [ValidateNever]
            public Status Status { get; set; } = null!;

      
    }
}
