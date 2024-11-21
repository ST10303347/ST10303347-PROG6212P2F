using Microsoft.AspNetCore.Identity;
using ST10303347_PROG6212P2F.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10303347_PROG6212P2F.Models
{
    public class SupportingDocument
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "File path cannot exceed 500 characters")]
        public string FilePath { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "File name cannot exceed 255 characters")]
        public string FileName { get; set; }

        [Required]
        public string? IdentityUserId { get; set; }

        [ForeignKey("IdentityUserId")]
        public IdentityUser? User { get; set; }

        public int? ClaimId { get; set; }

        [ForeignKey("ClaimId")]
        public Claim? Claim { get; set; }
    }
}
