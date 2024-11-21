using ST10303347_PROG6212P2F.Models;

namespace ST10303347_PROG6212P2F.Services
{
    public interface IClaimService 
    {
        Task Add(Claim claim);
        IQueryable<Claim> GetAll();
        IQueryable<Claim> GetMyClaims(string userId); 
        IQueryable<Claim> GetPendingClaims(); 
        Task<Claim> GetById(int? id);
        Task SaveChangesAsync();




    }
}
