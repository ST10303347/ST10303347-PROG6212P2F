using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ST10303347_PROG6212P2F.Models
{
    public class Comment
    {

        public int Id { get; set; }
        public string ActualComment { get; set; }

        [Required]
        public string? IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]
        public IdentityUser? User { get; set; }

        public int? ClaimId { get; set; }
        [ForeignKey("ClaimId")]
        public Claim? Listing { get; set; }
    }
}
