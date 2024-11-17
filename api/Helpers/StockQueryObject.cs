using System.ComponentModel.DataAnnotations;

namespace api.Helpers
{
    public class StockQueryObject
    {
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string SortBy { get; set; } = null;
        public bool IsDescending { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
