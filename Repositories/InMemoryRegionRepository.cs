using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class InMemoryRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public InMemoryRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>{
                new Region(){
                    Id=Guid.NewGuid(),
                    Code="FEZ",
                    Name="Fezan SHB"
                }
            };
        }
    }
}