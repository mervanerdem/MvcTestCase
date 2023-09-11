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
    public class StocksController : Controller
    {
        private readonly TestContext _context;

        public StocksController(TestContext context)
        {
            _context = context;
        }

        // GET: Stocks
        public async Task<IActionResult> Index()
        {
            var testContext = _context.Stocks.Include(s => s.Product);
            return View(await testContext.ToListAsync());
        }

    }
}
