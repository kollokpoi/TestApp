using System.ComponentModel.DataAnnotations;

namespace TestApplication.ViewModels
{
    public class ChangeCounterViewModel
    {
        public int ApartmentId { get; set; }
        [Display(Name = "Дата замены")]
        public DateTime DateOfChange { get; set; } = DateTime.Now;
        [Display(Name = "Последние показания")]
        public double OldValue { get; set; }
        [Display(Name = "Новый счетчик")]
        public Guid CounterTo { get; set; }
        public List<Guid> Counters { get; set; } = new();
    }
}
