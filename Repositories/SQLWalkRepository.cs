using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public SQLWalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await nZWalksDbContext.Walks.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteByIdAsync(Guid id)
        {
            var walk=await nZWalksDbContext.Walks.FindAsync(id);
            if(walk is null) return null;

            nZWalksDbContext.Walks.Remove(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await nZWalksDbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var walk=await nZWalksDbContext.Walks.FindAsync(id);
            return walk;
        }

        public async Task<Walk?> UpdateByIdAsync(Guid id, Walk walk)
        {
            var existingWalk=await nZWalksDbContext.Walks.FindAsync(id);
            if(existingWalk is null) return null;

            existingWalk.Name=walk.Name;
            existingWalk.Description=walk.Description;
            existingWalk.LengthInKm=walk.LengthInKm;
            existingWalk.WalkImageUrl=walk.WalkImageUrl;
            existingWalk.DifficultyId=walk.DifficultyId;
            existingWalk.RegionId=existingWalk.RegionId;

            await nZWalksDbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}