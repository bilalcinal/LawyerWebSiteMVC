using LawyerWebSiteMVC.Data;

namespace LawyerWebSiteMVC.Interface
{
    public interface ICommentService
    {
        Task<(bool, string)> CreateCommentAsync(Comment comment);
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(int commentId);
        Task<(bool, string)> UpdateCommentStatusAsync(int commentId, bool status);
    }
}
