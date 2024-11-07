using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Models.DTOs
{
    public class UpdateRegionRequestDto
    {
        [MaxLength(3,ErrorMessage ="The Character Length Shouldn't Be More Than 3")]
        [MinLength(3,ErrorMessage ="The Character Length Shouldn't Be Less Than 3")]
        public required string Code { get; set; }

        [MaxLength(100,ErrorMessage ="The Character Length Shouldn't Be More Than 100")]
        public required String Name { get; set; }
        public String? RegionImageUrl { get; set; }
    }
}