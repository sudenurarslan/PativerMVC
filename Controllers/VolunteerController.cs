using Microsoft.AspNetCore.Mvc;
using PativerMVC.Models;

namespace PativerMVC.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VolunteerController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                _context.Volunteers.Add(volunteer);
                await _context.SaveChangesAsync();
                ViewBag.Message = "Başvurunuz başarıyla alındı!";
                ModelState.Clear();
            }
            return View();
        }
    }
}