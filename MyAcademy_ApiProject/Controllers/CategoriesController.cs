using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAcademy_ApiProject.Context;
using MyAcademy_ApiProject.Entities;

namespace MyAcademy_ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiContext _context;
        public CategoriesController(ApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult CategoryList()
        {
            var values = _context.Categories.ToList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok("Ekleme başarılı");
        }
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Ok("Silme başarılı");
        }
        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            var existingCategory = _context.Categories.Find(category.CategoryId);
            if (existingCategory == null)
            {
                return NotFound();
            }
            existingCategory.CategoryName = category.CategoryName;
            _context.SaveChanges();
            return Ok("Güncelleme başarılı");
        }
    }
}
