using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PativerMVC.Models;

namespace PativerMVC.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var reports = await _context.Reports.ToListAsync();
            return View(reports);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalType,Location,Description,ImagePath")] Report report)
        {
            if (ModelState.IsValid)
            {
                report.ReportDate = DateTime.Now;
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(report);
        }
    }
}