using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Data.Entities
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }
        public string Abrv { get; set; }

        public VehicleMake Make { get; set; }
    }
}
