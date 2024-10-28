using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>>GetAllAsync();
        Task<Region?>GetByIdAsync(Guid id);
        Task<List<Region>>GetByCodeAsync(String code);
        Task<List<Region>>GetByNameAsync(String name);
        Task<Region>CreateRegionAsync(Region region);
        Task<Region?>DeleteRegionAsync(Guid id);
    }
}