using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAcademy_ApiProject.Context;
using MyAcademy_ApiProject.Entities;

namespace MyAcademy_ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApiContext _context;
        public ProductsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var values = _context.Products.ToList();
            return Ok(values);
        }   

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok("Ekleme başarılı");
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok("Silme başarılı");
        }
        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return Ok("Güncelleme başarılı");
        }
        [HttpGet("ProductCount")]
        public IActionResult ProductCount()
        {
            var count = _context.Products.Count();
            return Ok(count);
        }
        [HttpGet("ProductListWithCategory")]
        public IActionResult ProductListWithCategory()
        {
            var values=_context.Products.Include(y=> y.Category).Select(p => new {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductStock = p.ProductStock,
                ProductPrice = p.ProductPrice,
                IsCritic = p.IsCritic,
                CategoryName = p.Category.CategoryName
            }).ToList();
            return Ok(values);
        }
    }
}
