using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ST10303347_PROG6212P2F.ENUMS;
using ST10303347_PROG6212P2F.Attributes;

public class ClaimVM
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter the hours worked.")]
    [Range(0, double.MaxValue, ErrorMessage = "Hours worked must be a positive number.")]
    public double HoursWorked { get; set; }

    [Required(ErrorMessage = "Please enter the hourly rate.")]
    [Range(0, double.MaxValue, ErrorMessage = "Hourly rate must be a positive number.")]
    public double HourRate { get; set; }

    [NotMapped]
    public double Total => HoursWorked * HourRate;

    [Required(ErrorMessage = "The User ID is required.")]
    public string? IdentityUserId { get; set; }

    [ForeignKey("IdentityUserId")]
    public IdentityUser? User { get; set; }

    [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "File size must be less than 5 MB.")]
    [FileExtensions(Extensions = "pdf,doc,docx,txt,rtf,xlsx,csv,ppt,pptx", ErrorMessage = "Only document files are allowed.")]
    public IFormFile? SupportingDocument { get; set; }

    public Status Status { get; set; } = Status.Pending;

    [Required(ErrorMessage = "A claim description is required.")]
    [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
    public string Description { get; set; }
}


