using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcTestCase.Models;

namespace MvcTestCase.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly TestContext _context;

        public PurchasesController(TestContext context)
        {
            _context = context;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            var testContext = _context.SPGetAllPurchases.FromSqlRaw("EXECUTE dbo.SPGetAllPurchases");
            return View( testContext);
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Include(p => p.Customer)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Customertitle");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Quantity,Price,Amount,Date,CustomerId")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                if (purchase.Date == null)
                {
                    purchase.Date = DateTime.Now;
                }
                var stock = _context.Stocks.FirstOrDefault(s => s.ProductId == purchase.ProductId);
                if(stock != null)
                {
                    stock.Quantity += purchase.Quantity;
                    _context.Update(stock);
                }
                else
                {
                    stock = new Stock
                    {
                        ProductId = purchase.ProductId,
                        Quantity = purchase.Quantity,
                        Date = purchase.Date
                    };
                    _context.Add(stock);
                }

                _context.Add(purchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", purchase.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", purchase.ProductId);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Customertitle", purchase.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", purchase.ProductId);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Quantity,Price,Amount,Date,CustomerId")] Purchase purchase)
        {
            if (id != purchase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var purchaseOld = _context.Purchases.AsNoTracking().FirstOrDefault(p => p.Id == purchase.Id);
                    if (purchaseOld != null)
                    {
                        var stock = _context.Stocks.FirstOrDefault(s => s.ProductId == purchase.ProductId);
                        if (stock != null)
                        {
                            stock.Quantity -= purchaseOld.Quantity;
                            stock.Quantity += purchase.Quantity;
                            _context.Update(stock);
                        }
                        else
                        {
                            stock = new Stock
                            {
                                ProductId = purchase.ProductId,
                                Quantity = purchase.Quantity,
                                Date = purchase.Date
                            };
                            _context.Add(stock);
                        }
                    }
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", purchase.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", purchase.ProductId);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Include(p => p.Customer)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Purchases == null)
            {
                return Problem("Entity set 'TestContext.Purchases'  is null.");
            }
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase != null)
            {
                var stock = _context.Stocks.FirstOrDefault(s => s.ProductId == purchase.ProductId);
                if (stock != null)
                {
                    if(stock.Quantity < purchase.Quantity)
                    {
                        return Problem("Stock quantity is less than purchase quantity.");
                    }
                    stock.Quantity -= purchase.Quantity;
                    _context.Update(stock);
                }

                _context.Purchases.Remove(purchase);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
          return (_context.Purchases?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
