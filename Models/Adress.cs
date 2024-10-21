using System.ComponentModel.DataAnnotations;

namespace TestApplication.Models
{
    public class Adress
    {
        public int Id { get; set; }
        [MaxLength(5)]
        public string Number { get; set; } = string.Empty;
        public int StreetId { get; set; }
        public virtual Street Street { get; set; } = new();
    }
}
