using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApplication.Classes;
using TestApplication.Models;
using TestApplication.ViewModels;

namespace TestApplication.Controllers
{
    public class AdressesController(DatabaseContext _context) : Controller
    {
        private DatabaseContext context = _context;

        public async Task<IActionResult> CountersToCheck(int id)
        {
            var adress = await context.Adresses.FirstOrDefaultAsync(ad => ad.Id == id);
            if (adress == null) 
                return NotFound();

            var apartments = await context.Apartments.Where(a => a.AdressId == id 
                && a.Counter != null && a.Counter.NextCheckDate<DateTime.Now).ToListAsync();

            var model = new CountersToCheckViewModel();
            model.Adress = $"{adress.Street.Name}/{adress.Number}";
            foreach (var apartment in apartments)
            {
                model.Apartments.Add(new()
                {
                    Id = apartment.Id,
                    Number = apartment.Number,
                    Street = apartment.Adress.Street.Name,
                    HouseNumber = apartment.Adress.Number,
                });
            }
            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            var model = new AdressCreateViewModel();
            model.Streets = await context.Streets.ToListAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdressCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Streets = await context.Streets.ToListAsync();
                return View(model);
            }

            var street = await context.Streets.FirstOrDefaultAsync(x => x.Id == model.StreetId);
            if (street is null)
                return NotFound();

            var newAdress = new Adress()
            {
                StreetId = model.StreetId,
                Street = street,
                Number = model.Number
            };

            context.Adresses.Add(newAdress);
            await context.SaveChangesAsync();

            return RedirectToAction("Create","Apartment");
        }
        [HttpPost]
        public async Task<IActionResult> CreateStreet(string name)
        {
            var street = new Street();
            street.Name = name;
            context.Streets.Add(street);
            await context.SaveChangesAsync();
            return Ok(new { street.Id, street.Name });
        }
    }
}
