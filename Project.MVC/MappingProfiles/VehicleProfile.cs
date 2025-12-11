using AutoMapper;
using Project.Data.Entities;
using Project.MVC.ViewModels;


namespace Project.MVC.MappingProfiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap(); //Vehicel make
            // Vehicle model
        }
    }

}