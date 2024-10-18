using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10303347_PROG6212P2F.Models;
using ST10303347_PROG6212P2F.Services;

using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ST10303347_PROG6212P2F.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly IClaimService _claimService;
        private readonly ICommentsService _commentService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClaimsController(IClaimService claimService, ICommentsService commentService, IWebHostEnvironment webHostEnvironment)
        {
            _claimService = claimService;
            _commentService = commentService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Claims
        public async Task<IActionResult> Index()
        {
            var claims = _claimService.GetAll();
            return View(await claims.ToListAsync());
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClaimVM claimVM)
        {
            if (ModelState.IsValid)
            {
                string? filePath = null;

                if (claimVM.SupportingDocument != null && claimVM.SupportingDocument.Length > 0)
                {
                    if (claimVM.SupportingDocument.Length > 5 * 1024 * 1024) // Check if file size exceeds 5MB
                    {
                        ModelState.AddModelError("SupportingDocument", "File size must be less than 5MB.");
                        return View(claimVM);
                    }

                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Documents");
                    string fileName = Path.GetFileName(claimVM.SupportingDocument.FileName);
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
                    Total = claimVM.HoursWorked * claimVM.HourRate,
                    IdentityUserId = claimVM.IdentityUserId,
                    Status = claimVM.Status
                };

                await _claimService.Add(claim);
                return RedirectToAction(nameof(Index));
            }

            return View(claimVM);
        }


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
    }
}
