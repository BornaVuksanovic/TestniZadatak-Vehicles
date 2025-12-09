using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Data.Entities
{
    public class VehicleMake
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv {  get; set; } //abbreviation = skraćenica

        public ICollection<VehicleModel> Models { get; set; } = new List<VehicleModel>();
    }
}
