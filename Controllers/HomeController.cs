using Microsoft.AspNetCore.Mvc;
using PativerMVC.Models;

namespace PativerMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                RecentAnimals = _context.Animals
                    .Where(a => !a.IsAdopted)
                    .OrderByDescending(a => a.CreatedDate)
                    .Take(3)
                    .ToList(),
                ActiveCampaigns = _context.Campaigns
                    .Where(c => c.EndDate == null || c.EndDate > DateTime.Now)
                    .OrderByDescending(c => c.StartDate)
                    .Take(2)
                    .ToList()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}