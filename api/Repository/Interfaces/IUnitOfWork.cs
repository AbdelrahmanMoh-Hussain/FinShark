namespace api.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IStockRepository Stock { get; }
        ICommentRepository Comment { get; }
        Task SaveAsync();
    }
}
