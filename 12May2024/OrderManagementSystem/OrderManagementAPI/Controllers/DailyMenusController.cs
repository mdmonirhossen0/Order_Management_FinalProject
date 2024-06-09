using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Data;
using OrderManagementClassLibrary.DTOsForAPI;
using OrderManagementClassLibrary.Models;

namespace OrderManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DailyMenusController : ControllerBase
    {
        private readonly AppDbContext _db;

        public DailyMenusController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyMenu>>> GetDailyMenus()
        {
            //var dm = await _db.DailyMenus.ToListAsync();
            //return Ok(new { dm });

            var result = (from d in _db.DailyMenus
                          join r in _db.Recipes on d.RecipeId equals r.RecipeId
                          select new
                          {
                              d.DailyMenuId,
                              d.DailyMenuDate,
                              d.DemandQuantity,
                              d.CookedQuantity,
                              d.ServingQuantity,
                              d.Price,
                              d.Recipe
                          }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DailyMenu>> GetDailyMenu(int id)
        {
            var dm = await _db.DailyMenus.FirstOrDefaultAsync(x => x.DailyMenuId == id);
            if (dm != null)
            {
                var rc = (from r in _db.Recipes
                          join i in _db.RecipeItems on r.RecipeId equals i.RecipeId
                          where (r.RecipeId == dm.RecipeId)
                          select new
                          {
                              r.RecipeId,
                              r.RecipeName,
                              r.RecipeItems
                          });
                return Ok(new { dm, rc });

            }
            else
            {
                return BadRequest();
            }

            //var dailyMenu = (from d in _db.DailyMenus
            //                 join r in _db.Recipes on d.RecipeId equals r.RecipeId
            //                 where d.DailyMenuId == id
            //                 select new
            //                 {
            //                     d.DailyMenuId,
            //                     d.DailyMenuDate,
            //                     d.DemandQuantity,
            //                     d.CookedQuantity,
            //                     d.ServingQuantity,
            //                     d.Price,
            //                     d.Recipe
            //                 }).ToList();
            //return Ok(dailyMenu);
        }

        [HttpPost]
        public async Task<ActionResult<DailyMenu>> PostDailyMenu(DailyMenuDTO dailyMenuDTO)
        {
            DailyMenu dailyMenu = new DailyMenu()
            {
                DailyMenuDate =dailyMenuDTO.DailyMenuDate,
                DemandQuantity = dailyMenuDTO.DemandQuantity,
                Price = dailyMenuDTO.Price,
                RecipeId = dailyMenuDTO.RecipeId
            };
            _db.DailyMenus.Add(dailyMenu);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Daily menu posted successfully." });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchDailyMenu(int id, DailyMenuDTOAfter dailyMenuDTOAfter)
        {
            var dailyMenu = await _db.DailyMenus
       .Include(dm => dm.Recipe)
       .ThenInclude(r => r.RecipeItems)
       .ThenInclude(ri => ri.Item)
       .FirstOrDefaultAsync(dm => dm.DailyMenuId == id);

            if (dailyMenu == null)
            {
                return NotFound();
            }

            dailyMenu.CookedQuantity = dailyMenuDTOAfter.CookedQuantity;

            var requiredItemQuantities = dailyMenu.Recipe.RecipeItems.ToDictionary(
                ri => ri.Item.ItemId,
                ri => ri.Quantity * dailyMenuDTOAfter.CookedQuantity
            );
            foreach (var itemQuantity in requiredItemQuantities)
            {
                var stock = await _db.Stocks.FirstOrDefaultAsync(s => s.ItemId == itemQuantity.Key);
                if (itemQuantity.Value <= stock.Quantity)
                {
                    if (stock != null)
                    {
                        stock.Quantity -= itemQuantity.Value;
                        _db.Stocks.Update(stock);
                    }
                }
                else
                {
                    throw new Exception("Not enough stock quantiy");
                }
               
                
            }

            await _db.SaveChangesAsync();

            return Ok(new { dailyMenu });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>PutDailyMenu(int id,DailyMenu dailyMenu)
        {
            if (id != dailyMenu.DailyMenuId)
            {
                return BadRequest();
            }

            _db.Entry(dailyMenu).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyMenuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool DailyMenuExists(int id)
        {
            return _db.DailyMenus.Any(e => e.DailyMenuId == id);
        }
        
    
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyMenu(int id)
        {
            var dailyMenu = await _db.DailyMenus.FindAsync(id);
            if (dailyMenu == null)
            {
                return NotFound();
            }

            if(dailyMenu != null)
            {
                _db.DailyMenus.Remove(dailyMenu);
                await _db.SaveChangesAsync();
            }

            return Ok(new {message= $"Daily menu {id} deleted successfully." });
        }
    }
}
