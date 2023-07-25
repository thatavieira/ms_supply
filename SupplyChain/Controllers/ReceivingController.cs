using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Data;
using SupplyChain.Enums;
using SupplyChain.Models;
using SupplyChain.ViewModels;

namespace SupplyChain.Controllers
{
    public class ReceivingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceivingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Receiving
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InventoryManagements.Where(i => i.Type == MovementType.Inbound).Include(i => i.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Receiving/Details/5
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

        // GET: Receiving/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description");
            return View();
        }

        // POST: Receiving/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReceivingViewModel viewModel )
        {
            var receiving = new InventoryManagement(viewModel.ProductId, viewModel.Local, viewModel.Type, viewModel.Quantity);
            
            if (ModelState.IsValid)
            {
                _context.Add(receiving);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description");
            return View(receiving);
        }
        

        private bool InventoryManagementExists(int id)
        {
          return (_context.InventoryManagements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
