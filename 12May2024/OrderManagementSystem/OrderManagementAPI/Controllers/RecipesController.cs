using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OrderManagementAPI.Data;
using OrderManagementClassLibrary.DTOsForAPI;
using OrderManagementClassLibrary.Models;

namespace OrderManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public RecipesController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _db.Recipes.Include(x=>x.RecipeItems).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipeById(int id)
        {
            var recipe = await _db.Recipes.Include(x=>x.RecipeItems).FirstOrDefaultAsync(x=>x.RecipeId==id);

            if (recipe == null)
            {
                return NotFound();
            }
            return recipe;
        }

        [HttpPost]
        public async Task<IActionResult> PostRecipe([FromBody] RecipeDTO recipeDTO)
        {
            Recipe recipe = new Recipe()
            {
                RecipeName = recipeDTO.RecipeName
            };
            _db.Recipes.Add(recipe);
            await _db.SaveChangesAsync();
            List<RecipeItem> recipeItems = JsonConvert.DeserializeObject<List<RecipeItem>>(recipeDTO.RecipeItems);
            var recipeObj = _db.Recipes.FirstOrDefault(r => r.RecipeName == recipeDTO.RecipeName);
            var recipeId = recipeObj.RecipeId;
            AddRecipeItem(recipeId, recipeItems);
            await _db.SaveChangesAsync();
            return Ok(new {message= "Recipe saved successfully." });
        }

        private void AddRecipeItem(int recipeId, List<RecipeItem>? recipeItems)
        {
            if(recipeItems.Any())
            {
                foreach(var item in recipeItems)
                {
                    RecipeItem recipeItem = new RecipeItem()
                    {
                        RecipeId = recipeId,
                        Quantity = item.Quantity,
                        ItemId = item.ItemId,
                    };
                    _db.RecipeItems.Add(recipeItem);
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, [FromBody] RecipeDTO recipeDTO)
        {
            var recipe = await _db.Recipes.FindAsync(id);
            if(recipe == null)
            {
                return NotFound("Recipe data not found.");
            }
            recipe.RecipeName = recipeDTO.RecipeName;
            var existingRecipeItem = _db.RecipeItems.Where(ri => ri.RecipeId == id);
            _db.RecipeItems.RemoveRange(existingRecipeItem);
            List<RecipeItem> list = JsonConvert.DeserializeObject<List<RecipeItem>>(recipeDTO.RecipeItems);
            AddRecipeItem(recipe.RecipeId, list);
            await _db.SaveChangesAsync();
            return Ok(new {message="Recipe Updated successfully"});
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _db.Recipes.FindAsync(id);
            if(recipe == null)
            {
                return BadRequest("Recipe data not found.");
            }

            if(recipe != null)
            {
                _db.Recipes.Remove(recipe);
                await _db.SaveChangesAsync();
            }

            return Ok(new {message="Recipe Deleted"});
        }
    }
}
