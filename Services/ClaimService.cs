using Microsoft.EntityFrameworkCore;
using ST10303347_PROG6212P2F.ENUMS;
using ST10303347_PROG6212P2F.Models;
using ST10303347_PROG6212P2F.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ST10303347_PROG6212P2F.Data.Services
{
    public class ClaimService : IClaimService
    {
        private readonly ApplicationDbContext _context;

        public ClaimService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Claim claim)
        {
            _context.claims.Add(claim); // Ensure correct DbSet name
            await _context.SaveChangesAsync();
        }

        public IQueryable<Claim> GetAll()
        {
            return _context.claims
                .Include(c => c.User)
                .Include(c => c.Comments)
                .Include(c => c.SupportingDocuments);
        }

        public IQueryable<Claim> GetMyClaims(string userId)
        {
            return _context.claims
                .Where(c => c.IdentityUserId == userId)
                .Include(c => c.User)
                .Include(c => c.Comments)
                .Include(c => c.SupportingDocuments);
        }

        public IQueryable<Claim> GetPendingClaims()
        {
            return _context.claims
                .Where(c => c.Status == Status.Pending)
                .Include(c => c.User)
                .Include(c => c.Comments)
                .Include(c => c.SupportingDocuments);
        }

        public async Task<Claim> GetById(int? id)
        {
            return await _context.claims
                .Include(c => c.User)
                .Include(c => c.Comments)
                .Include(c => c.SupportingDocuments)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}

