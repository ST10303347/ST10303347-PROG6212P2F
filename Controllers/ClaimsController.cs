using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10303347_PROG6212P2F.Models;
using ST10303347_PROG6212P2F.Services;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ST10303347_PROG6212P2F.ENUMS;
using Claim = ST10303347_PROG6212P2F.Models.Claim;
using NSubstitute.Exceptions;

namespace ST10303347_PROG6212P2F.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly IClaimService _claimService;
        private readonly ICommentsService _commentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IClaimReportService _claimReportService;

        public ClaimsController(
            IClaimService claimService,
            ICommentsService commentService,
            IWebHostEnvironment webHostEnvironment,
            IClaimReportService claimReportService)
        {
            _claimService = claimService;
            _commentService = commentService;
            _webHostEnvironment = webHostEnvironment;
            _claimReportService = claimReportService;
        }

        // GET: Claims (All Claims)
        public async Task<IActionResult> Index()
        {
            var claims = _claimService.GetAll();
            return View(await claims.ToListAsync());
        }

        // GET: Claims/PendingClaims (Pending Claims for coordinators/managers)
        public async Task<IActionResult> PendingClaims()
        {
            var pendingClaims = _claimService.GetPendingClaims();
            return View(await pendingClaims.ToListAsync());
        }

        // GET: Claims/MyClaims (Claims for the current lecturer)
        public async Task<IActionResult> MyClaims()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myClaims = _claimService.GetMyClaims(userId);
            return View(await myClaims.ToListAsync());
        }

        // GET: Claims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var claim = await _claimService.GetById(id);
            if (claim == null)
                return NotFound();

            return View(claim);
        }

        // GET: Claims/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Claims/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClaimVM claimVM)
        {
            if (ModelState.IsValid)
            {
                string? filePath = null;
                string? fileName = null;

                if (claimVM.SupportingDocument != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Documents");
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    fileName = Path.GetFileName(claimVM.SupportingDocument.FileName);
                    filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await claimVM.SupportingDocument.CopyToAsync(stream);
                    }
                }

                var claim = new Claim
                {
                    HoursWorked = claimVM.HoursWorked,
                    HourRate = claimVM.HourRate,
                    Total = claimVM.Total,
                    IdentityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Status = Status.Pending,
                    Comments = new List<Comment>(), // Initialize empty list
                    SupportingDocuments = fileName != null
                        ? new List<SupportingDocument>
                        {
                    new SupportingDocument
                    {
                        FileName = fileName,
                        FilePath = filePath
                    }
                        }
                        : null
                };

                await _claimService.Add(claim);
                return RedirectToAction("MyClaims", "Claims");
            }

            return View(claimVM);
        }


        // POST: Claims/AddComment
        [HttpPost]
        public async Task<IActionResult> AddComment([Bind("Id, ActualComment, ClaimId, IdentityUserId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                await _commentService.Add(comment);
            }

            var claim = await _claimService.GetById(comment.ClaimId);
            return View("Details", claim);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var claim = await _claimService.GetById(id);
            if (claim == null) return NotFound();

            claim.Status = Status.Approved;
            await _claimService.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var claim = await _claimService.GetById(id);
            if (claim == null) return NotFound();

            claim.Status = Status.Rejected;
            await _claimService.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> GenerateClaimReport(int claimId)
        {
            try
            {
                var reportBytes = await _claimReportService.GenerateClaimReportAsync(claimId);
                var fileName = $"ClaimReport_{claimId}.pdf";

                return File(reportBytes, "application/pdf", fileName);
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ErrorPage"); 
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An unexpected error occurred.";
                return RedirectToAction("ErrorPage"); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> MassAction(string filterType, string name, double? minPay, double? maxPay, double? minHours, double? maxHours, string action)
        {
            var claims = await _claimService.GetAll().ToListAsync();

            if (filterType == "name" && !string.IsNullOrEmpty(name))
            {
                claims = claims.Where(c => c.IdentityUserId.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else if (filterType == "criteria")
            {
                claims = claims.Where(c =>
                    c.HourRate >= minPay &&
                    c.HourRate <= maxPay &&
                    c.HoursWorked >= minHours &&
                    c.HoursWorked <= maxHours
                ).ToList();
            }

            if (action == "approve")
            {
                foreach (var claim in claims)
                {
                    claim.Status = Status.Approved;
                }
            }
            else if (action == "reject")
            {
                foreach (var claim in claims)
                {
                    claim.Status = Status.Rejected;
                }
            }

            await _claimService.SaveChangesAsync(); 
            TempData["Success"] = $"{claims.Count} claims have been {action}d.";
            return RedirectToAction("PendingClaims");
        }




    }

}


