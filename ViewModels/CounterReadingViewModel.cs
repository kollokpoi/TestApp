using System.ComponentModel.DataAnnotations;

namespace TestApplication.ViewModels
{
    public class CounterReadingViewModel
    {
        public Guid CounterId { get; set; }

        [Display(Name = "Показатели")]
        [Required(ErrorMessage = "Укажите показатели")]
        public double Value { get; set; }

        [Display(Name = "Дата снятия показаний")]
        public DateTime DateOfReading { get; set; } = DateTime.Now;
    }
}
