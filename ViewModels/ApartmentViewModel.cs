using System.ComponentModel.DataAnnotations;
using TestApplication.Models;

namespace TestApplication.ViewModels
{
    public class ApartmentViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Номер квартиры")]
        public string Number { get; set; } = "";

        [Display(Name = "Улица")]
        public int StreetId { get; set; }
        public string Street { get; set; } = "";

        [Display(Name = "Номер дома")]
        public int HouseId { get; set; }
        public string HouseNumber { get; set; } = "";

        public CounterViewModel? Counter { get; set; }
        public List<CounterStory> CounterStories { get; set; } = new();
    }
}
