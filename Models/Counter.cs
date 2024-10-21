namespace TestApplication.Models
{
    public class Counter
    {
        public Guid Id { get; set; }
        public DateTime? LastCheckDate { get; set; }
        public DateTime NextCheckDate { get; set; }

        public int? ApartmentId { get; set; }
        public virtual Apartment? Apartment { get; set; }

        public virtual List<CounterReading> CounterReadings { get; set; } = new();
    }
}
