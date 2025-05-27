using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PativerMVC.Models;

namespace PativerMVC.Controllers
{
    public class AdoptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdoptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Create(int animalId)
        {
            var animal = _context.Animals.FirstOrDefault(a => a.Id == animalId);
            if (animal == null)
            {
                return NotFound();
            }

            var model = new AdoptionRequest
            {
                AnimalId = animalId
            };

            ViewBag.AnimalName = animal.Name;
            return View(model);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdoptionRequest request)
        {
            if (ModelState.IsValid)
            {
                _context.AdoptionRequests.Add(request);
                var animal = await _context.Animals.FindAsync(request.AnimalId);
                if (animal != null)
                {
                    animal.IsAdopted = true;
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Animal");
            }

            return View(request);
        }

        }
    }
