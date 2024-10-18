using ST10303347_PROG6212P2F.Models;

namespace ST10303347_PROG6212P2F.Services
{
    public interface ICommentsService
    {
        Task Add(Comment comment);
    }
}
