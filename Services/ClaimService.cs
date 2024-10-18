using Microsoft.EntityFrameworkCore;
using ST10303347_PROG6212P2F.Models;
using ST10303347_PROG6212P2F.Services;

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
            _context.claims.Add(claim);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Claim> GetAll()
        {
            var claims = _context.claims
                .Include(c => c.User)
                .Include(c => c.Comments)
                .Include(c => c.SupportingDocuments);
            return claims;
        }

        public async Task<Claim> GetById(int? id)
        {
            var claim = await _context.claims
                .Include(c => c.User)
                .Include(c => c.Comments)
                .Include(c => c.SupportingDocuments)
                .FirstOrDefaultAsync(m => m.Id == id);
            return claim;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}

