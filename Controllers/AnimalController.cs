using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PativerMVC.Models;
using PativerMVC.Services;

namespace PativerMVC.Controllers
{
    public class AnimalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public AnimalController(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        
        public async Task<IActionResult> Index()
        {
            var animals = await _context.Animals
                .Where(a => !a.IsAdopted)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
            return View(animals);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var animal = await _context.Animals.FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
                return NotFound();

            return View(animal);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Age,Breed,Description")] Animal animal, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine("wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    
                    animal.ImagePath = "/images/" + fileName;
                }
                else
                {
                    ModelState.AddModelError("ImagePath", "Bir görsel seçmelisiniz.");
                    return View(animal);
                }

                animal.CreatedDate = DateTime.Now;
                animal.IsAdopted = false;

                _context.Animals.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(animal);
        }


        
        public async Task<IActionResult> Adopt(int? id)
        {
            if (id == null)
                return NotFound();

            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
                return NotFound();

            var model = new AdoptionRequest
            {
                AnimalId = animal.Id,
                Animal = animal
            };

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adopt(int id, [Bind("AnimalId,FullName,Phone")] AdoptionRequest adoptionRequest)
        {
            if (id != adoptionRequest.AnimalId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Form geçersiz:");
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                return View(adoptionRequest);
            }

            adoptionRequest.RequestDate = DateTime.Now;
            adoptionRequest.IsApproved = false;

            _context.AdoptionRequests.Add(adoptionRequest);

            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                animal.IsAdopted = true;
                _context.Update(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

