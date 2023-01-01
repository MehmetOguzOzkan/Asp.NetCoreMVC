using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectWEB.Models;

namespace ProjectWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        Context context = new Context();
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            var y = await context.Categories.ToListAsync();
            if (y is null)
            {
                return NoContent();
            }
            return y;

        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var y = await context.Categories.FirstOrDefaultAsync(x => x.CategoryID == id);
            if (y is null)
            {
                return NoContent();
            }
            return y;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category category)
        {
            var selectedCategory = context.Categories.FirstOrDefault(c=>c.CategoryID==id);
            if (selectedCategory is null)
            {
                return NotFound();
            }
            selectedCategory.CategoryName = category.CategoryName;
            selectedCategory.CategoryDescription = category.CategoryDescription;
            selectedCategory.CategoryImage = category.CategoryImage;
            selectedCategory.CategoryState = category.CategoryState;
            context.Update(selectedCategory);
            context.SaveChanges();
            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var selectedCategory = context.Categories.FirstOrDefault(c=>c.CategoryID==id);
            if (selectedCategory is null)
            {
                return NotFound();
            }
            if (selectedCategory.Foods.Any(x => x.CategoryID == id))
            {
                return NotFound("Kategoriye ait ürün yok");
            }
            context.Categories.Remove(selectedCategory);
            context.SaveChanges();
            return Ok();
        }

    }
}
