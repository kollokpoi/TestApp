using System.ComponentModel.DataAnnotations;

namespace TestApplication.Models
{
    public class CounterReading
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime DateOfReading { get; set; }
        public virtual Counter Counter { get; set; } = new();
    }
}
