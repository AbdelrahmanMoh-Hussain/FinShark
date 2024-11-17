using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Repository.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(StockQueryObject query);
        Task<Stock> GetByIdAsync(int id);
        Task AddAsync(Stock stock);
        void Update(Stock oldStock, StockRequestDto newStock);
        void Delete(Stock stock);

        Task<bool> CheckExistAsync(int? id);
    }
}
