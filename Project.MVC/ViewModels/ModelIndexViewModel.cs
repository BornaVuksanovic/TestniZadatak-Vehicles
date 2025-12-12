namespace Project.MVC.ViewModels
{
    public class ModelIndexViewModel
    {
        public IEnumerable<ModelIndexViewModel> Items { get; set; } = new List<ModelIndexViewModel>();

        public int? MakeId { get; set; }
        public string? SearchingString { get; set; }

        public string? CurrentSort { get; set; }
        public string NameSortParm { get; set; } = "";
        public string AbrvSortParm { get; set; } = "";
        public string MakeSortParm { get; set; } = "";

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public List<MakeDropdownItem> Makes { get; set; } = new();

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }

    public class MakeDropdownItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
}

