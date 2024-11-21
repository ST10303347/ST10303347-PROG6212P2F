using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.AspNetCore.Hosting;
using ST10303347_PROG6212P2F.Models;
using System.Data;

namespace ST10303347_PROG6212P2F.Services
{
    public class ClaimReportService : IClaimReportService
    {
        private readonly IClaimService _claimService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClaimReportService(IClaimService claimService, IWebHostEnvironment webHostEnvironment)
        {
            _claimService = claimService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<byte[]> GenerateClaimReportAsync(int claimId)
        {
            var claim = await _claimService.GetById(claimId);
            if (claim == null)
                throw new ArgumentException("Claim not found");

            var reportDocument = new ReportDocument();
            reportDocument.Load(Path.Combine(_webHostEnvironment.WebRootPath, "Reports", "ClaimReport.rpt"));

            var claimTable = new DataTable();
            claimTable.Columns.Add("Id", typeof(int));
            claimTable.Columns.Add("HoursWorked", typeof(double));
            claimTable.Columns.Add("HourRate", typeof(double));
            claimTable.Columns.Add("Total", typeof(double));
            claimTable.Columns.Add("Status", typeof(string));

            claimTable.Rows.Add(
                claim.Id,
                claim.HoursWorked,
                claim.HourRate,
                claim.Total,
                claim.Status.ToString()
            );

            reportDocument.SetDataSource(claimTable);

            using (var reportStream = reportDocument.ExportToStream(ExportFormatType.PortableDocFormat))
            {
                using (var memoryStream = new MemoryStream())
                {
                    reportStream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        public async Task<byte[]> GenerateBulkClaimReportAsync(List<Claim> claims)
        {
            var reportDocument = new ReportDocument();
            reportDocument.Load(Path.Combine(_webHostEnvironment.WebRootPath, "Reports", "BulkClaimReport.rpt"));

            var claimsTable = new DataTable();
            claimsTable.Columns.Add("Id", typeof(int));
            claimsTable.Columns.Add("HoursWorked", typeof(double));
            claimsTable.Columns.Add("HourRate", typeof(double));
            claimsTable.Columns.Add("Total", typeof(double));
            claimsTable.Columns.Add("Status", typeof(string));

            foreach (var claim in claims)
            {
                claimsTable.Rows.Add(
                    claim.Id,
                    claim.HoursWorked,
                    claim.HourRate,
                    claim.Total,
                    claim.Status.ToString()
                );
            }

            reportDocument.SetDataSource(claimsTable);

            using (var reportStream = reportDocument.ExportToStream(ExportFormatType.PortableDocFormat))
            {
                using (var memoryStream = new MemoryStream())
                {
                    reportStream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
