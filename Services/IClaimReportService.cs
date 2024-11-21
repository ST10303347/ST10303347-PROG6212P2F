using ST10303347_PROG6212P2F.Models;

namespace ST10303347_PROG6212P2F.Services
{
    public interface IClaimReportService
    {
        Task<byte[]> GenerateClaimReportAsync(int claimId);
        Task<byte[]> GenerateBulkClaimReportAsync(List<Claim> claims);

    }
}
