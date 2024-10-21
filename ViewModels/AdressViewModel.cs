using System.ComponentModel.DataAnnotations;
using TestApplication.Models;

namespace TestApplication.ViewModels
{
    public class AdressViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Номер дома")]
        public string Number { get; set; } = string.Empty;
        [Display(Name = "Номер улицы")]
        public int StreetId { get; set; }
        [Display(Name = "Улица")]
        public string StreetName { get; set; } = string.Empty;
        public List<Street> Streets { get; set; } = new();
    }
}
