using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication10.Data;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Inventory
        public async Task<IActionResult> Index()
        {
            var inventoryItems = await _context.Inventories.ToListAsync();
            return View(inventoryItems);
        }

        // GET: Inventory/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: Inventory/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddInventoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var inventoryItem = new Inventory
                {
                    ItemName = model.ItemName,
                    QuantityAvailable = model.QuantityAvailable
                };

                _context.Inventories.Add(inventoryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Inventory/Update/5
        public async Task<IActionResult> Update(int id)
        {
            var inventoryItem = await _context.Inventories.FindAsync(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            var model = new UpdateInventoryViewModel
            {
                Id = inventoryItem.Id,
                ItemName = inventoryItem.ItemName,
                QuantityAvailable = inventoryItem.QuantityAvailable
            };
            return View(model);
        }

        // POST: Inventory/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateInventoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var inventoryItem = await _context.Inventories.FindAsync(model.Id);
                if (inventoryItem == null)
                {
                    return NotFound();
                }

                inventoryItem.ItemName = model.ItemName;
                inventoryItem.QuantityAvailable = model.QuantityAvailable;

                _context.Inventories.Update(inventoryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        // GET: Inventory/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var inventoryItem = await _context.Inventories.FindAsync(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }
            return View(inventoryItem);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventoryItem = await _context.Inventories.FindAsync(id);
            if (inventoryItem != null)
            {
                _context.Inventories.Remove(inventoryItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}