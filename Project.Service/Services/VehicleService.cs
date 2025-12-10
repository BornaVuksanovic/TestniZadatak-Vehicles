using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Data.Entities;
using Project.Service.Interfaces;

namespace Project.Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleDbContext _context;

        public VehicleService(VehicleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleMake>> GetVehicleMakesAsync()
        {
            return await _context.VehicleMakes.ToListAsync();
        }

        public async Task<VehicleMake?> GetMakeByIdAsync(int id)
        {
            return await _context.VehicleMakes.FindAsync(id);
        }

        public async Task AddMakeAsync(VehicleMake make)
        {
            _context.VehicleMakes.Add(make);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMakeAsync(VehicleMake make)
        {
            _context.VehicleMakes.Update(make);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMakeAsync(int id)
        {
            var make = await _context.VehicleMakes.FindAsync(id);
            if (make == null) return;

            _context.VehicleMakes.Remove(make);
            await _context.SaveChangesAsync();
        }
    }
}
