using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApplication.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string Number { get; set; } = "";

        public int AdressId { get; set; }
        public virtual Adress Adress { get; set; } = new();

        public int? CounterId { get; set; }
        public virtual Counter? Counter { get; set; }

        public virtual List<CounterStory> CounterStory { get; set; } = new();
    }
}
