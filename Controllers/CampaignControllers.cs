using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PativerMVC.Models;

namespace PativerMVC.Controllers
{
    public class CampaignController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CampaignController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var campaigns = await _context.Campaigns.ToListAsync();
            return View(campaigns);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var campaign = await _context.Campaigns.FirstOrDefaultAsync(c => c.Id == id);
            if (campaign == null)
                return NotFound();

            return View(campaign);
        }
    }
}