using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Data;
using SupplyChain.Enums;
using SupplyChain.Models;
using SupplyChain.ViewModels;
using X.PagedList;

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
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10; 
            int pageNumber = page;
            
            var applicationDbContext = _context.InventoryManagements
                .Where(i => i.Type == MovementType.Outbound)
                .Include(i => i.Product)
                .OrderByDescending(i => i.Id);
            
            return View(await applicationDbContext.ToPagedListAsync(pageNumber, pageSize));
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Shipping/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShippingViewModel viewModel)
        {

            var shipping =
                new InventoryManagement(viewModel.ProductId, viewModel.Local, viewModel.Type, viewModel.Quantity);
      
            var inbound =  _context.InventoryManagements
                .Where(i => i.Type == MovementType.Inbound)
                .Where(i => i.ProductId == shipping.ProductId)
                .Select(i => i.Quantity).Sum();
            
            var outbound =  _context.InventoryManagements
                .Where(i => i.Type == MovementType.Outbound)
                .Where(i => i.ProductId == shipping.ProductId)
                .Select(i => i.Quantity).Sum();

            var totalOutbound = outbound + shipping.Quantity;
            
            if (ModelState.IsValid &  inbound >= totalOutbound)
            {
                _context.Add(shipping);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["productExists"] = "exists";
                ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description");
                return View(shipping);
            }
        }
        
        private bool InventoryManagementExists(int id)
        {
          return (_context.InventoryManagements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
