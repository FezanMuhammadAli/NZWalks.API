using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController: ControllerBase
    {
        // GET ALL REGIONS
        // Get: https://localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAll(){
            var regions=new List<Region>{
                new Region{
                    Id=Guid.NewGuid(),
                    Name="Auckland Region",
                    Code="AKL",
                    RegionImageUrl="https://www.pexels.com/photo/auckland-skyline-with-sky-tower-and-ocean-view-29015601/"
                },
                new Region{
                    Id=Guid.NewGuid(),
                    Name="Wellington Region",
                    Code="WLG",
                    RegionImageUrl="https://www.pexels.com/photo/group-hiking-adventure-in-wellington-new-zealand-29049909/"
                }
            };
            return Ok(regions);
        }
    }
}