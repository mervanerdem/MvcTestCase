using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcTestCase.Models;

namespace MvcTestCase.Controllers
{
    public class ReportController : Controller
    {
        private readonly TestContext _context;

        public ReportController(TestContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var testContext = _context.SPReportGetAllSalesDetails.FromSqlRaw("EXECUTE dbo.SPReportGetAllSalesDetail");
            return View(testContext);
        }
    }
}
