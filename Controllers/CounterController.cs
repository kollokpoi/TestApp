using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApplication.Classes;
using TestApplication.Models;
using TestApplication.ViewModels;

namespace TestApplication.Controllers
{
    public class CounterController(DatabaseContext _context) : Controller
    {
        private DatabaseContext context = _context;

        public async Task<IActionResult> Index()
        {
            var viewModel = new HouseListViewModel();
            var adressesList = await context.Adresses.ToListAsync();
            viewModel.Streets = await context.Streets.ToListAsync();

            foreach (var item in adressesList)
            {
                var itemViewModel = new AdressViewModel()
                {
                    Id = item.Id,
                    Number = item.Number,
                    StreetId = item.StreetId,
                    StreetName = item.Street.Name,
                };
                viewModel.Adresses.Add(itemViewModel);
            }

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View(new CounterViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CounterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.LastCheckDate>model.NextCheckDate)
            {
                return View(model);
            }
            var newCounter = new Counter()
            {
                Id = model.Id, 
                LastCheckDate = model.LastCheckDate,
                NextCheckDate = model.NextCheckDate,
            };
            newCounter.CounterReadings.Add(new()
            {
                DateOfReading = DateTime.Now,
                Counter = newCounter,
                Value = model.CurrentValue,
            });
            await context.Counters.AddAsync(newCounter);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateValue(Guid id)
        {
            var model = new CounterReadingViewModel();
            model.CounterId = id;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateValue(CounterReadingViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var counter = await context.Counters.FirstOrDefaultAsync(x => x.Id == model.CounterId);
            if (counter is null)
                return View(model);

            if (counter.CounterReadings.Count > 0)
            {
                if (counter.CounterReadings.Last().Value > model.Value)
                {
                    ModelState.AddModelError("value", "Значение должно быть больше последнего.");
                    return View(model);
                }
            }

            counter.CounterReadings.Add(new()
            {
                Counter = counter,
                DateOfReading = model.DateOfReading,
                Value = model.Value,
            });
            await context.SaveChangesAsync();
            return RedirectToAction("Details","Apartment",new { Id = counter.ApartmentId });
        }
    }
}
