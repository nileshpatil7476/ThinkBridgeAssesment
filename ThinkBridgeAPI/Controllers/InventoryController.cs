using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThinkBridgeAPI.Models;

namespace ThinkBridgeAPI.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoriesController : Controller
    {
        private readonly DatabaseContext _context;

        public InventoriesController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventoryDetails()
        {

            return await _context.Inventories.ToListAsync();
        }

        // GET: api/inventory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventoryDetails(int id)
        {
            var InventoryDetails = await _context.Inventories.FindAsync(id);

            if (InventoryDetails == null)
            {
                return NotFound();
            }

            return InventoryDetails;
        }

        // PUT: api/inventory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryDetails(int id, Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            _context.Entry(inventory).State = EntityState.Modified;

            try
            {
                inventory.LastUpdatedOn = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
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

        // POST: api/inventory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventoryDetails(Inventory inventory)
        {
            inventory.CreatedOn = DateTime.Now;
            inventory.LastUpdatedOn = DateTime.Now;
            _context.Inventories.Add(inventory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return CreatedAtAction("GetInventoryDetails", new { id = inventory.Id }, inventory);
        }

        // DELETE: api/inventory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var InventoryDetails = await _context.Inventories.FindAsync(id);
            if (InventoryDetails == null)
            {
                return NotFound();
            }

            _context.Inventories.Remove(InventoryDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventories.Any(e => e.Id == id);
        }
    }
}
