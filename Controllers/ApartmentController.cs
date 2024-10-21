using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestApplication.Classes;
using TestApplication.Models;
using TestApplication.ViewModels;

namespace TestApplication.Controllers
{
    public class ApartmentController(DatabaseContext _context) : Controller
    {
        private DatabaseContext context = _context;

        public async Task<IActionResult> Index()
        {
            var viewModel = new ApartmentListViewModel();
            var apartmentsList = await context.Apartments.OrderBy(x=>x.AdressId).ToListAsync();
            viewModel.Streets = await context.Streets.ToListAsync();

            foreach (var item in apartmentsList)
            {
                var itemViewModel = new ApartmentViewModel()
                {
                    Id = item.Id,
                    Number = item.Number
                };
                if (item.Counter is not null)
                {
                    itemViewModel.Counter = new();
                    var lastReading = item.Counter.CounterReadings.Last();
                    itemViewModel.Counter.Id = item.Counter.Id;
                    itemViewModel.Counter.NextCheckDate = item.Counter.NextCheckDate;
                    itemViewModel.Counter.LastCheckDate = item.Counter.LastCheckDate;
                    itemViewModel.Counter.CurrentValue = lastReading.Value;
                }

                itemViewModel.Street = item.Adress.Street.Name;
                itemViewModel.StreetId = item.Adress.StreetId;

                itemViewModel.HouseNumber = item.Adress.Number;
                itemViewModel.HouseId = item.Adress.Id;

                viewModel.Apartmments.Add(itemViewModel);
            }
            
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var apartment = await context.Apartments.FirstOrDefaultAsync(x=>x.Id == id);
            if (apartment is null)
                return NotFound();

            var itemViewModel = new ApartmentViewModel()
            {
                Id = apartment.Id,
                Number = apartment.Number
            };
            if (apartment.Counter is not null)
            {
                itemViewModel.Counter = new();
                var lastReading = apartment.Counter.CounterReadings.Last();
                itemViewModel.Counter.Id = apartment.Counter.Id;
                itemViewModel.Counter.NextCheckDate = apartment.Counter.NextCheckDate;
                itemViewModel.Counter.LastCheckDate = apartment.Counter.LastCheckDate;
                itemViewModel.Counter.CurrentValue = lastReading.Value;
            }

            itemViewModel.Street = apartment.Adress.Street.Name;
            itemViewModel.StreetId = apartment.Adress.StreetId;

            itemViewModel.HouseNumber = apartment.Adress.Number;
            itemViewModel.HouseId = apartment.Adress.Id;

            itemViewModel.CounterStories = apartment.CounterStory;

            return View(itemViewModel);
        }

        public async Task<IActionResult> ChangeCounter(int id)
        {
            var apartment = await context.Apartments.FirstOrDefaultAsync(x => x.Id == id);
            if (apartment is null)
                return NotFound();

            var model = new ChangeCounterViewModel();
            model.ApartmentId = apartment.Id;
            model.Counters = await context.Counters
                .Where(x => x.Apartment == null)
                .Select(x=>x.Id).ToListAsync();

            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            var viewModel = new ApartmentCreateViewModel();
            var adresses = await context.Adresses.ToListAsync();
            foreach (var ad in adresses)
            {
                viewModel.Adresses.Add(new()
                {
                    Id = ad.Id,
                    Number = ad.Number,
                    StreetId = ad.StreetId,
                    StreetName = ad.Street.Name,
                });
            }
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeCounter(ChangeCounterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var apartment = await context.Apartments.FirstOrDefaultAsync(x => x.Id == model.ApartmentId);
            if (apartment is null)
                return NotFound();

            var counter = await context.Counters.FirstOrDefaultAsync(x => x.Id == model.CounterTo);
            if (counter is null)
                return NotFound();

            if(apartment.Counter is not null)
            {
                apartment.Counter.CounterReadings.Add(new()
                {
                    DateOfReading = DateTime.Now,
                    Value = model.OldValue
                });
                await context.SaveChangesAsync();
            }
            counter.Apartment = apartment;
            apartment.Counter = counter;
            apartment.CounterStory.Add(new()
            {
                Apartment = apartment,
                CounterToID = model.CounterTo,
                DateOFChange = model.DateOfChange,
                OlderValue = model.OldValue
                
            });
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = model.ApartmentId});
        }
        [HttpPost]
        public async Task<IActionResult> Create(ApartmentCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var adress = await context.Adresses.FirstOrDefaultAsync(x => x.Id == model.AdressId);
            if (adress is null)
                return NotFound();

            var newApartment = new Apartment();
            newApartment.Adress = adress;
            newApartment.Number = model.Number;

            context.Apartments.Add(newApartment);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
