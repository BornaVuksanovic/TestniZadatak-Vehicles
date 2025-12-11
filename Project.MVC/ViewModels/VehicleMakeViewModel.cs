using System.ComponentModel.DataAnnotations;

namespace Project.MVC.ViewModels
{
    public class VehicleMakeViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string Abrv { get; set; } = string.Empty;


    }
}
