using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;
using api.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

 

        public async Task<List<Stock>> GetAllAsync(StockQueryObject query)
        {
            var stocks = _context.Stocks.Include(x => x.Comments)
                .AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.CompanyName))
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));

            if (!string.IsNullOrWhiteSpace(query.Symbol))
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));

            if (!string.IsNullOrWhiteSpace(query.SortBy)) {
                stocks = query.IsDescending 
                    ? stocks.OrderByDescending(s =>  s.Symbol ) 
                    : stocks.OrderBy(s => s.Symbol);
            }

            var skippedNumber = (query.PageNumber) * query.PageSize;

            return await stocks.Skip(skippedNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(s => s.Comments)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
        }
        public void Update(Stock oldStock, StockRequestDto newStock)
        {
            _context.Stocks.Entry(oldStock).CurrentValues.SetValues(newStock);
        }


        public void Delete(Stock stock)
        {
            _context.Remove(stock);
        }

        public async Task<bool> CheckExistAsync(int? id)
        {
            return await _context.Stocks.AnyAsync(x => x.Id == id);
        }
    }
}
