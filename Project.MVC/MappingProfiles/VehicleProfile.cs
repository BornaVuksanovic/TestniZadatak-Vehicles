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

            CreateMap<VehicleModel, VehicleModelViewModel>()
                .ForMember(d => d.MakeName,
                    opt => opt.MapFrom(s => s.Make.Name));

            CreateMap<VehicleModelViewModel, VehicleModel>()
                .ForMember(d => d.Make, opt => opt.Ignore());                                          // Vehicle model
        }
    }

}