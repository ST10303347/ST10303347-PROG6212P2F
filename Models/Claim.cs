using Microsoft.AspNetCore.Identity;
using ST10303347_PROG6212P2F.ENUMS;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ST10303347_PROG6212P2F.Models
{
    public class Claim
    {
        public int Id { get; set; }

        [Required]
        public string? IdentityUserId { get; set; }

        [ForeignKey("IdentityUserId")]
        public IdentityUser? User { get; set; }

        public double HoursWorked { get; set; }
        public double HourRate { get; set; }
        public double Total { get; set; }
        public Status Status { get; set; }

        public List<Comment>? Comments { get; set; }
        public List<SupportingDocument>? SupportingDocuments { get; set; }
    }
}
