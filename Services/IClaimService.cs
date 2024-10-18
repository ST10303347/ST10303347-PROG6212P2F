using ST10303347_PROG6212P2F.Models;

namespace ST10303347_PROG6212P2F.Services
{
    public interface IClaimService
    {
        IQueryable<Claim> GetAll();
        Task Add(Claim claim);
        Task<Claim> GetById(int? id);
        Task SaveChanges();





    }
}
