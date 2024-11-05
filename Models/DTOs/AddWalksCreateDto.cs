using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Models.DTOs
{
    public class AddWalksCreateDto
    {
         public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        //adding foreign keys
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}