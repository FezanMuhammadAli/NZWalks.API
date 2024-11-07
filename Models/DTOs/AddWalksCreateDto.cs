using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Models.DTOs
{
    public class AddWalksCreateDto
    {
         public required string Name { get; set; }
        public required string Description { get; set; }
        public required double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        //adding foreign keys
        public required Guid DifficultyId { get; set; }
        public required Guid RegionId { get; set; }
    }
}