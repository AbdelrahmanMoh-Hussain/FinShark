using api.Data;
using api.Dtos.Comment;
using api.Models;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }
        public async Task<Comment> AddAsync(Comment comment)
        {
            if (await CheckStockExistAsync(comment.StockId))
            {
                await _context.AddAsync(comment);
                return comment;
            }
            else
                return null;
        }
        public void Update(Comment oldComment, UpdateCommentDto newComment)
        {
            _context.Entry(oldComment).CurrentValues.SetValues(newComment);
        }
        public void Delete(Comment stock)
        {
            _context.Remove(stock);
        }

        public async Task<bool> CheckStockExistAsync(int? id)
        {
            return await _context.Stocks.AnyAsync(x => x.Id == id);

        }
    }
}
