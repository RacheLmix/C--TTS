using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogApi.Data;
using ProductCatalogApi.DTOs;
using AutoMapper;

namespace ProductCatalogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductCatalogContext _context;
        private readonly IMapper _mapper;
        public ProductsController(ProductCatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? name, [FromQuery] string? category, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name));
            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(x => x.Category == category);
            if (minPrice.HasValue)
                query = query.Where(x => x.Price >= minPrice);
            if (maxPrice.HasValue)
                query = query.Where(x => x.Price <= maxPrice);
            var total = await query.CountAsync();
            var products = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return Ok(new { total, data = _mapper.Map<List<ProductDto>>(products) });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, _mapper.Map<ProductDto>(product));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            _mapper.Map(dto, product);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{id}/sell")]
        public async Task<IActionResult> Sell(int id, [FromQuery] int quantity)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            if (product.Stock < quantity) return BadRequest("Không đủ tồn kho!");
            product.Stock -= quantity;
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpGet("report/stock-value")]
        public async Task<IActionResult> GetStockValue()
        {
            var totalValue = await _context.Products.SumAsync(x => x.Price * x.Stock);
            return Ok(new { totalValue });
        }
    }
} 