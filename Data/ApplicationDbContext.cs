using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ST10303347_PROG6212P2F.Models;

namespace ST10303347_PROG6212P2F.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Claim> claims { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<SupportingDocument> supportingDocuments { get; set; }
    }
}
