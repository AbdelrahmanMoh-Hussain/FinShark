using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class StockRequestDto
    {
        [MaxLength(12, ErrorMessage = "Length must be at most 12 chars")]
        public string Symbol { get; set; } = string.Empty;

        [MinLength(5, ErrorMessage = "Length must be at least 5 chars")]
        [MaxLength(200, ErrorMessage = "Length must be at most 200 chars")]
        public string CompanyName { get; set; } = string.Empty;

        [Range(1, 1_000_000)]        
        public decimal Purchase { get; set; }

        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }

        [MinLength(5, ErrorMessage = "Length must be at least 5 chars")]
        [MaxLength(200, ErrorMessage = "Length must be at most 200 chars")]
        public string Industry { get; set; } = string.Empty;

        [Range(1, 1_000_000)]
        public string MarketCap { get; set; } = string.Empty;

    }
}