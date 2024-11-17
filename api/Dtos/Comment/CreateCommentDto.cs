using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {
        [MinLength(5, ErrorMessage = "Title must be at least 5 chars")]
        [MaxLength(200, ErrorMessage = "Title must be at most 200 chars")]
        public string Title { get; set; } = string.Empty;

        [MinLength(5, ErrorMessage = "Content must be at least 5 chars")]
        [MaxLength(200, ErrorMessage = "Content must be at most 200 chars")]
        public string Content { get; set; } = string.Empty;
        public int? StockId { get; set; }
    }
}
