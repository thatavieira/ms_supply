using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Data;
using SupplyChain.Models;

namespace SupplyChain.Controllers
{
    public class ShippingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShippingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shipping
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InventoryManagements.Include(i => i.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Shipping/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InventoryManagements == null)
            {
                return NotFound();
            }

            var inventoryManagement = await _context.InventoryManagements
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventoryManagement == null)
            {
                return NotFound();
            }

            return View(inventoryManagement);
        }

        // GET: Shipping/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description");
            return View();
        }

        // POST: Shipping/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,CreateAt,UpdateAt,Local,Type,Quantity")] InventoryManagement inventoryManagement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventoryManagement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", inventoryManagement.ProductId);
            return View(inventoryManagement);
        }

        // GET: Shipping/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InventoryManagements == null)
            {
                return NotFound();
            }

            var inventoryManagement = await _context.InventoryManagements.FindAsync(id);
            if (inventoryManagement == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", inventoryManagement.ProductId);
            return View(inventoryManagement);
        }

        // POST: Shipping/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,CreateAt,UpdateAt,Local,Type,Quantity")] InventoryManagement inventoryManagement)
        {
            if (id != inventoryManagement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryManagementExists(inventoryManagement.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", inventoryManagement.ProductId);
            return View(inventoryManagement);
        }

        // GET: Shipping/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InventoryManagements == null)
            {
                return NotFound();
            }

            var inventoryManagement = await _context.InventoryManagements
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventoryManagement == null)
            {
                return NotFound();
            }

            return View(inventoryManagement);
        }

        // POST: Shipping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InventoryManagements == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InventoryManagements'  is null.");
            }
            var inventoryManagement = await _context.InventoryManagements.FindAsync(id);
            if (inventoryManagement != null)
            {
                _context.InventoryManagements.Remove(inventoryManagement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryManagementExists(int id)
        {
          return (_context.InventoryManagements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
