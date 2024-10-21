namespace TestApplication.ViewModels
{
    public class CountersToCheckViewModel
    {
        public string Adress { get; set; } = string.Empty;
        public List<ApartmentViewModel> Apartments { get; set; } = new();
    }
}
