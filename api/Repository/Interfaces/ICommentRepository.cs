using api.Dtos.Comment;
using api.Dtos.Stock;
using api.Models;

namespace api.Repository.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> AddAsync(Comment stock);
        void Update(Comment oldComment, UpdateCommentDto newComment);
        void Delete(Comment stock);
        Task<bool> CheckStockExistAsync(int? id);
    }
}
