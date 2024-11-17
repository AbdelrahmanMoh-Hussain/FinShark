using api.Dtos.Comment;
using api.Mappers;
using api.Models;
using api.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CommentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _unitOfWork.Comment.GetAllAsync();
            var commentsDto = _mapper.Map<List<CommentDto>>(comments);
            return Ok(commentsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _unitOfWork.Comment.GetByIdAsync(id);
            if (comment == null)
                return NotFound();
            var commentDto = _mapper.Map<CommentDto>(comment);

            return Ok(commentDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto model)
        {
            var comment = _mapper.Map<Comment>(model);

            if (await _unitOfWork.Stock.CheckExistAsync(model.StockId))
                await _unitOfWork.Comment.AddAsync(comment);
            else
                return BadRequest($"Stock with id:{model.StockId} does not exist!");

            await _unitOfWork.SaveAsync();
            var commentDto = _mapper.Map<CommentDto>(comment);

            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, commentDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentDto model)
        {
            var comment = await _unitOfWork.Comment.GetByIdAsync(id);
            if (comment == null)
                return NotFound();

            _unitOfWork.Comment.Update(oldComment: comment, newComment: model);
            await _unitOfWork.SaveAsync();
            var commentDto = _mapper.Map<CommentDto>(comment);

            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, commentDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Comment = await _unitOfWork.Comment.GetByIdAsync(id);
            if (Comment == null)
                return NotFound();

            _unitOfWork.Comment.Delete(Comment);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
