using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PativerMVC.Models;

namespace PativerMVC.Controllers
{
    public class DonationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonationController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Create(int? campaignId = null)
        {
            ViewBag.CampaignId = campaignId;
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonorName,Email,Amount,CampaignId")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                donation.DonationDate = DateTime.Now;
                _context.Add(donation);

                
                if (donation.CampaignId.HasValue)
                {
                    var campaign = await _context.Campaigns.FindAsync(donation.CampaignId.Value);
                    if (campaign != null)
                    {
                        campaign.CollectedAmount += donation.Amount;
                        _context.Campaigns.Update(campaign);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Donation");
            }

            return View(donation);
        }


        
        public async Task<IActionResult> Index()
        {
            var donations = await _context.Donations
                .OrderByDescending(d => d.DonationDate)
                .ToListAsync();

            ViewBag.Total = donations.Sum(d => d.Amount);
            return View(donations);
        }
    }
}