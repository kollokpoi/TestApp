using System.ComponentModel.DataAnnotations;

namespace TestApplication.Models
{
    public class CounterStory
    {
        public int Id { get; set; }
        public DateTime DateOFChange { get; set; }
        public int ApartmentId { get; set; }
        public virtual Apartment Apartment { get; set; } = new();
        [MaxLength(10)]
        public double? OlderValue { get; set; }
        public Guid? CounterToID { get; set; }
        public virtual Counter? CounterTo { get; set; } = null;
    }
}
