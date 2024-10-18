using Microsoft.AspNetCore.Mvc;
using ST10303347_PROG6212P2F.Models;
using ST10303347_PROG6212P2F.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ST10303347_PROG6212P2F.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int claimId, string actualComment)
        {
            if (string.IsNullOrWhiteSpace(actualComment))
            {
                ModelState.AddModelError(string.Empty, "Comment cannot be empty.");
                return RedirectToAction("Details", "Claims", new { id = claimId });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var comment = new Comment
            {
                ActualComment = actualComment,
                IdentityUserId = userId,
                ClaimId = claimId
            };

            await _commentsService.Add(comment);
            return RedirectToAction("Details", "Claims", new { id = claimId });
        }
    }
}
