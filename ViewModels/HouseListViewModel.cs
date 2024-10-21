using TestApplication.Models;

namespace TestApplication.ViewModels
{
    public class HouseListViewModel
    {
        public List<AdressViewModel> Adresses { get; set; } = new();
        public List<Street> Streets { get; set; } = new();
    }
}
