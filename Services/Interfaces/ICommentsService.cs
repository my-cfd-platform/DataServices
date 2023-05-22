using CommentFlows;

namespace DataServices.Services.Interfaces
{
    public interface ICommentsService
    {
        Task PostCommentAsync(CommentGrpcModel model);
        Task<IEnumerable<CommentGrpcModel>> GetCommentsAsync(string categoryId);
    }
}