using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FindAsync(id);
        }

        public async Task<List<Region>> GetByCodeAsync(string code)
        {
            return await dbContext.Regions.Where(x=>x.Code==code).ToListAsync();
        }

        public async Task<List<Region>> GetByNameAsync(string name)
        {
            return await dbContext.Regions.Where(x=>x.Name.Contains(name)).ToListAsync();
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var region=await dbContext.Regions.FindAsync(id);
            if(region is null)
            {
                return null;
            }
            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateByIdAsync(Guid id, Region region)
        {
            var regionModel=await dbContext.Regions.FindAsync(id);
            if(regionModel is null) return null;

            regionModel.Code=region.Code;
            regionModel.Name=region.Name;
            regionModel.RegionImageUrl=region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return regionModel; 
        }
    }
}