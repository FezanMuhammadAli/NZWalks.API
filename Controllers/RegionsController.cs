using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController: ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL REGIONS          #1
        [HttpGet]
        public IActionResult GetAll(){
            //using Domain-Models retrieving data from DB
            var regionsDomain=dbContext.Regions.ToList();
            //Map Domain-Models to DTO
            var regionDto=new List<RegionDto>();
            foreach(var regionDomain in regionsDomain){
                regionDto.Add(new RegionDto(){
                    Code=regionDomain.Code,
                    Name=regionDomain.Name,
                    RegionImageUrl=regionDomain.RegionImageUrl
                });
            }
            //return DTO
            return Ok(regionDto);
        }

        // GET REGION BY ID         #2
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute]Guid id){
            // var region=dbContext.Regions.Find(id);
            //retrieving data from db using Domain-Models
            var regionDomain=dbContext.Regions.FirstOrDefault(x=>x.Id==id);
            if(regionDomain==null){
                return NotFound();
            }
            //map domain-model to dto
            var regionDto=new RegionDto{
                Code=regionDomain.Code,
                Name=regionDomain.Name,
                RegionImageUrl=regionDomain.RegionImageUrl
            };
            return Ok(regionDto);
        }

        // GET REGIONS BY CODE          #3
        [HttpGet]
        [Route("code/{code}")]
        public IActionResult GetByCode([FromRoute]String code){
            var regions=dbContext.Regions.Where(x=>x.Code==code).ToList();
            if(!regions.Any()){
                return NotFound();
            }
            return Ok(regions);
        }

        // GET REGIONS BY NAME          #4
        [HttpGet]
        [Route("{name}")]
        public IActionResult GetByName([FromRoute]string name){
            var regions=dbContext.Regions.Where(x=>x.Name.Contains(name)).ToList();
            if(!regions.Any()){
                return NotFound();
            }
            return Ok(regions);
        }
    }
}