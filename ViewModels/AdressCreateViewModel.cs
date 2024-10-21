using System.ComponentModel.DataAnnotations;
using TestApplication.Models;

namespace TestApplication.ViewModels
{
    public class AdressCreateViewModel
    {
        [Display(Name = "Номер дома")]
        public string Number { get; set; } = string.Empty;
        [Display(Name = "Улица")]
        public int StreetId { get; set; }
        public List<Street> Streets { get; set; } = new();
    }
}
