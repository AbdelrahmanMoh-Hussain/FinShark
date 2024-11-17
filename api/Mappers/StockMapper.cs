using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMapper
    {
        public static StockDto ToStockDto (this Stock model)
        {
            return new StockDto {
                Id = model.Id,
                Symbol = model.Symbol,
                Purchase = model.Purchase,
                LastDiv = model.LastDiv,
                CompanyName = model.CompanyName,
                MarketCap = model.MarketCap,
                Industry = model.Industry
            };
        }
        public static Stock ToStockFromRequestDto (this StockRequestDto model)
        {
            return new Stock {
                Symbol = model.Symbol,
                Purchase = model.Purchase,
                LastDiv = model.LastDiv,
                CompanyName = model.CompanyName,
                MarketCap = model.MarketCap,
                Industry = model.Industry
            };
        }
    }
}