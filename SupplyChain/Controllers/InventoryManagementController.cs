
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupplyChain.Data;

namespace SupplyChain.Controllers
{
    public class InventoryManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int productId, DateTime initial, DateTime final)
        {
            
            ViewData["productId"] = new SelectList(_context.Products, "Id", "Name");

            if (productId == 0 || initial == DateTime.MinValue || final == DateTime.MinValue)
            {
                return View();
            }
            else
            {
                DateTime initialDataUtc = initial.ToUniversalTime();
                DateTime finalDataUtc = final.ToUniversalTime();
                string initialDateFormat = initialDataUtc.ToString("yyyy-MM-dd");
                string finalDateFormat = finalDataUtc.ToString("yyyy-MM-dd");

                
                string sqlQuery = $@"
                with rangeDay as (
	                select
	                extract(day from day) as day
	                from generate_series('{initialDateFormat}'::date, '{finalDateFormat}'::date, '1 day') as day
                ),

                receiving_quantity as (
	                select 
	                ""Quantity"", 
                    extract(day from ""CreateAt"") as day  
                    from ""InventoryManagements"" im
                    where im.""CreateAt"" between '{initialDateFormat}' and '{finalDateFormat}'
                    and im.""ProductId"" = {productId}
                    and im.""Type"" = 0
                ),

                shipping_quantity as (
                    select 
                    ""Quantity"", 
                    extract(day from ""CreateAt"") as day  
                    from ""InventoryManagements"" im
                    where im.""CreateAt"" between '{initialDateFormat}' and '{finalDateFormat}'
                    and im.""ProductId"" = {productId}
                    and im.""Type"" = 1
                )

                select 
                rd.day::integer as day,
                coalesce(sum(rq.""Quantity""), 0)::integer as receiving,
                coalesce(sum(sq.""Quantity""), 0)::integer as shipping
                from rangeDay rd
                left join receiving_quantity rq on rd.day = rq.day
                left join shipping_quantity sq on rd.day = sq.day
                group by rd.day 
                order by rd.day
            ";

                var product = _context.Products.FirstOrDefault(i => i.Id == productId);

                var chartData =
                    _context.InventoryManagementChart.FromSqlRaw(sqlQuery, productId, initialDateFormat, finalDateFormat).ToList();

                List<int> day = new List<int>();
                List<int> receiving = new List<int>();
                List<int> shipping = new List<int>();


                foreach (var data in chartData)
                {
                    day.Add(data.Day);
                    receiving.Add(data.Receiving);
                    shipping.Add(data.Shipping);
                }

                ViewData["Day"] = day;
                ViewData["Receiving"] = receiving;
                ViewData["Shipping"] = shipping;
                ViewData["product"] = $"{product.Id} - {product.Name}";
                ViewData["initialDate"] = $"{initial.Day}/{initial.Month}/{initial.Year}";
                ViewData["finalDate"] = $"{final.Day}/{final.Month}/{final.Year}";

                string sqlQueryInventory = $@"
                    select * from ""InventoryManagements"" im
                    where im.""CreateAt"" between '{initialDateFormat}' and '{finalDateFormat}'
                    and im.""ProductId"" = {productId} order by im.""CreateAt"" asc";
                
                var applicationDbContext = _context
                    .InventoryManagements.FromSqlRaw(sqlQueryInventory);
                    
                return View(await applicationDbContext.ToListAsync());
                
            }

        }

        private bool InventoryManagementExists(int id)
        {
          return (_context.InventoryManagements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
