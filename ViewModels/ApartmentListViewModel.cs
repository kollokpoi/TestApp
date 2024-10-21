using TestApplication.Models;

namespace TestApplication.ViewModels
{
    public class ApartmentListViewModel
    {
        public List<Street> Streets { get; set; } = new();
        public List<ApartmentViewModel> Apartmments { get; set; } = new();
    }
}
