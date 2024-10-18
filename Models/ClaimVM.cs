using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ST10303347_PROG6212P2F.ENUMS;

public class ClaimVM
{
    public int Id { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Hours worked must be a positive number.")]
    public double HoursWorked { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Hourly rate must be a positive number.")]
    public double HourRate { get; set; }

    public double Total => HoursWorked * HourRate; 

    [Required]
    public string? IdentityUserId { get; set; }

    [ForeignKey("IdentityUserId")]
    public IdentityUser? User { get; set; }


    [FileExtensions(Extensions = "pdf,doc,docx,txt,rtf,xlsx,csv,ppt,pptx",
     ErrorMessage = "Only document files are allowed.")]
    public IFormFile? SupportingDocument { get; set; }

    public Status Status { get; set; }
}

