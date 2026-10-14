using Microsoft.AspNetCore.Mvc;
using MyAcademy_ApiWebUIProject.Dtos.CategoryDtos;
using Newtonsoft.Json;

namespace MyAcademy_ApiWebUIProject.Controllers
{
    public class CategoryController : Controller
    {
        public async Task<IActionResult> CategoryList()
        {
            var client = new HttpClient();
            var responseMessage = await client.GetAsync("https://localhost:7124/api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var client = new HttpClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent stringContent = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7124/api/Categories", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CategoryList");
            }
            // Logic for creating a category
            return View(createCategoryDto);
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = new HttpClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7124/api/Categories?={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CategoryList");
            }
            return View();
        }
        [HttpGet]
        public IActionResult UpdateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var client = new HttpClient();
            var responseMessage = await client.PutAsync("https://localhost:7124/api/Categories", new StringContent(JsonConvert.SerializeObject(updateCategoryDto), System.Text.Encoding.UTF8, "application/json"));
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CategoryList");
            }
            return View();
        }
    }
}
