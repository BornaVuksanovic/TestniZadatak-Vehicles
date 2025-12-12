using System.ComponentModel.DataAnnotations;

namespace Project.MVC.ViewModels
{
    public class VehicleModelViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Make")]
        public int MakeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abrv { get; set; }

        public string MakeName { get; set; }

    }
}
