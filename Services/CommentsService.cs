using ST10303347_PROG6212P2F.Models;
using ST10303347_PROG6212P2F.Services;

namespace ST10303347_PROG6212P2F.Data.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext _context;

        public CommentsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Comment comment)
        {
            _context.comments.Add(comment);
            await _context.SaveChangesAsync();
        }
    }
}
