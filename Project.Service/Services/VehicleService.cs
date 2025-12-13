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
            return await _context.VehicleMakes.OrderBy(m => m.Name).ToListAsync();
        }

        public async Task<(IEnumerable<VehicleMake> Items, int TotalCount)>
            GetMakesAsync(string? searchString, string? sortOrder, int pageNumber, int pageSize)
        {
            var query = _context.VehicleMakes.AsQueryable();

            // pretraga filter
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(m => m.Name.Contains(searchString));
            }

            // sortiranje

            query = sortOrder switch
            {
                "name_desc" => query.OrderByDescending(m => m.Name),
                "abrv" => query.OrderBy(m => m.Abrv),
                "abrv_desc" => query.OrderByDescending(m => m.Abrv),
                _ => query.OrderBy(m => m.Name) // defaultni
            };

            var totalCount =  await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
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




        public async Task<(IEnumerable<VehicleModel> Items, int TotalCount)>
            GetModelsAsync(int? makeId, string? searchString, string? sortOrder, int pageNumber, int pageSize)
        {
            var query = _context.VehicleModels
                .Include(m => m.Make)
                .AsQueryable();

            if (makeId.HasValue)
            {
                query = query.Where(m => m.MakeId == makeId.Value);
            }

            if(!string.IsNullOrWhiteSpace(searchString))
                query = query.Where(m => m.Name.Contains(searchString));

            query = sortOrder switch
            {
                "name_desc" => query.OrderByDescending(m => m.Name),
                "abrv" => query.OrderBy(m => m.Abrv),
                "abrv_desc" => query.OrderByDescending(m => m.Abrv),
                "make" => query.OrderBy(x => x.Make.Name),
                "make_desc" => query.OrderByDescending(x => x.Make.Name),
                _ => query.OrderBy(m => m.Name)
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return(items, totalCount);
        }

        public async Task<VehicleModel?> GetModelByIdAsync(int id)
        {
            return await _context.VehicleModels.FindAsync(id);
        }

        public async Task AddModelAsync(VehicleModel model)
        {
            _context.VehicleModels.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateModelAsync(VehicleModel model)
        { 
            _context.VehicleModels.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteModelAsync(int id)
        {
            var model = await _context.VehicleModels.FindAsync(id);
            if (model != null)
            {
                _context.VehicleModels.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

    }
}
