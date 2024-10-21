using System.ComponentModel.DataAnnotations;

namespace TestApplication.Models
{
    public class Street
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public virtual List<Adress> Houses { get; set; } = new();
    }
}
