using System;
using System.Collections.Generic;
using System.Text;

using Project.Data.Entities;

namespace Project.Service.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleMake>> GetVehicleMakesAsync();
        Task<VehicleMake?> GetMakeByIdAsync(int id);

        Task AddMakeAsync(VehicleMake make);
        Task UpdateMakeAsync(VehicleMake make);
        Task DeleteMakeAsync(int id);

        Task<(IEnumerable<VehicleMake> Items, int TotalCount)>
            GetMakesAsync(string? searchString, string? sortOrder, int pageNumber, int pageSize);

    }
}
