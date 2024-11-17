using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Mappers;
using api.Models;
using api.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StockController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] StockQueryObject query)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var stocks = await _unitOfWork.Stock.GetAllAsync(query);
            //var stocks = _mapper.Map<List<StockDto>>(await _unitOfWork.Stock.GetAllAsync());
            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id == 0)
                return BadRequest();

            var stock = await _unitOfWork.Stock.GetByIdAsync(id);
            if(stock == null)
                return NotFound();

            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StockRequestDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = _mapper.Map<Stock>(model);
            await _unitOfWork.Stock.AddAsync(stock);
            await _unitOfWork.SaveAsync();

            var stockDto = _mapper.Map<StockDto>(stock);

            return CreatedAtAction(nameof(GetById), new {id = stock.Id}, stockDto);
        }

        [HttpPut("{id:int}")]
        public async Task< IActionResult> Update(int id, [FromBody] StockRequestDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _unitOfWork.Stock.GetByIdAsync(id);
            if(stock == null)
                return NotFound();

            _unitOfWork.Stock.Update(oldStock: stock, newStock: model);
            await _unitOfWork.SaveAsync();

            var stockDto = _mapper.Map<StockDto>(stock);

            return CreatedAtAction(nameof(GetById), new {id = stock.Id}, stockDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0)
                return BadRequest("Id cannot be 0");

            var stock = await _unitOfWork.Stock.GetByIdAsync(id);
            if (stock == null)
                return NotFound();
            
           _unitOfWork.Stock.Delete(stock);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}