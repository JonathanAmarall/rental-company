﻿using Microsoft.EntityFrameworkCore;
using RentalCompany.Core.Data;
using RentalCompany.Domain.Entities;
using RentalCompany.Domain.Repositories;

namespace RentalCompany.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly RentalCompanyContext _context;

        public LocationRepository(RentalCompanyContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task CreateAsync(Location location)
        {
            await _context.Locations!.AddAsync(location);
        }

        public void Delete(Location location)
        {
            _context.Locations!.Remove(location);
        }


        public async Task<Location?> GetByIdAsync(Guid id)
        {
            return await _context.Locations!
                .Include(x => x.CollectionItems)
                .Include(x => x.Childrens)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Location>?> GetLocationsChildrenAsync(Guid id)
        {
            return await _context.Locations!
                .Include(x => x.Childrens)
                .Where(x => x.Childrens != null && x.Id == id)
                .ToListAsync();
        }

        public async Task<string> GetFullLocationTag(Guid id)
        {
            string tag = "";

            Location? currentLocation = await _context.Locations!.AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.Parent)
                .FirstOrDefaultAsync();

            if (currentLocation == null)
                return tag;

            tag += $"{currentLocation.Description}";

            while (currentLocation?.ParentId != null)
            {
                currentLocation = await _context.Locations!
                .AsNoTracking()
                .Include(x => x.Parent)
                .ThenInclude(x => x!.Parent)
                .Where(x => x.Id == currentLocation.ParentId)
                .FirstOrDefaultAsync();

                tag = $"{currentLocation?.Description} > {tag}";
            }

            return tag;
        }

        public async Task<List<Location>> GetRootsAsync()
        {
            var locations = await _context.Locations!
                 .Where(x => x.ParentId == null)
                 .AsNoTracking()
                 .ToListAsync();

            return locations;
        }

        public void Update(Location location)
        {
            _context.Locations!.Update(location);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }
    }
}
