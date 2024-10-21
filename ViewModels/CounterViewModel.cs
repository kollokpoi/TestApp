using System.ComponentModel.DataAnnotations;
using TestApplication.Helpers.Attributes;

namespace TestApplication.ViewModels
{
    public class CounterViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Display(Name = "Дата прошлой поверки")]
        public DateTime? LastCheckDate { get; set; }
        [Display(Name = "Дата следующей поверки")]
        [Required(ErrorMessage = "Укажите дату следующей поверки")]
        [DateBiggerThan(nameof(LastCheckDate))]
        public DateTime NextCheckDate { get; set; }
        [Required(ErrorMessage = "Текущее значение обязательно")]
        public double CurrentValue { get; set; }
    }
}
