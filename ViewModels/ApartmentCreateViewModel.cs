using System.ComponentModel.DataAnnotations;
using TestApplication.Models;

namespace TestApplication.ViewModels
{
    public class ApartmentCreateViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Номер квартиры")]
        public string Number { get; set; } = "";
        [Display(Name = "Адрес")]
        public int AdressId { get; set; }
        public List<AdressViewModel> Adresses { get; set; } = new();
    }
}
