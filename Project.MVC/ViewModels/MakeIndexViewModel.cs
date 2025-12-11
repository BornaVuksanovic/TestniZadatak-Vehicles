namespace Project.MVC.ViewModels
{
    public class MakeIndexViewModel
    {
        public IEnumerable<VehicleMakeViewModel> Makes {  get; set; } = new List<VehicleMakeViewModel>();

        public string? CurrentFilter { get; set; }
        public string? CurrentSort { get; set; }
        
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

    }
}
