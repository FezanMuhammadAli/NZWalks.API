using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>>GetAllAsync();
        Task<Region>GetByIdAsync(Guid id);
        Task<List<Region>>GetByCodeAsync(String code);
    }
}