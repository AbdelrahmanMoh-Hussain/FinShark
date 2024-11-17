using api.Data;
using api.Repository.Interfaces;

namespace api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Stock = new StockRepository(context);
            Comment = new CommentRepository(context);
    }

        public IStockRepository Stock {  get; private set; }

        public ICommentRepository Comment {  get; private set; }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
