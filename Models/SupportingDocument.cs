using Microsoft.AspNetCore.Identity;
using ST10303347_PROG6212P2F.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10303347_PROG6212P2F.Models
{
    public class SupportingDocument
    {
        public int Id { get; set; }

        public string FilePath { get; set; }

        [Required]
        public string? IdentityUserId { get; set; }

        [ForeignKey("IdentityUserId")]
        public IdentityUser? User { get; set; }

        public int? ClaimId { get; set; }

        [ForeignKey("ClaimId")]
        public Claim? Claim { get; set; }
    }
}
