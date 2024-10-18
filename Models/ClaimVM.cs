using Microsoft.AspNetCore.Identity;
using ST10303347_PROG6212P2F.ENUMS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10303347_PROG6212P2F.Models
{
    public class ClaimVM
    {
        public int Id { get; set; }
        public double HoursWorked { get; set; }
        public double HourRate { get; set; }
        public double Total { get; set; }

        [Required]
        public string? IdentityUserId { get; set; }

        [ForeignKey("IdentityUserId")]
        public IdentityUser? User { get; set; }

        public IFormFile? SupportingDocument { get; set; }
        public Status Status { get; set; }
    }
}
