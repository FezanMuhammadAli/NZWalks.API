using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Models.DTOs
{
    public class RegionDto
    {
        public string Code { get; set; }
        public String Name { get; set; }
        public String? RegionImageUrl { get; set; }
    }
}