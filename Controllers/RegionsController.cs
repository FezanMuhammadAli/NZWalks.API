using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController: ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET ALL REGIONS          #1
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            //using Domain-Models retrieving data from DB
            var regionsDomain=await regionRepository.GetAllAsync();
            //Map Domain-Models to DTO
            // var regionDto=new List<RegionDto>();
            // foreach(var regionDomain in regionsDomain){
            //     regionDto.Add(new RegionDto(){
            //         Id=regionDomain.Id,
            //         Code=regionDomain.Code,
            //         Name=regionDomain.Name,
            //         RegionImageUrl=regionDomain.RegionImageUrl
            //     });
            // }
            var regionDto=mapper.Map<List<RegionDto>>(regionsDomain);
            //return DTO
            return Ok(regionDto);
        }

        // GET REGION BY ID         #2
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id){
            // var region=dbContext.Regions.Find(id);
            //retrieving data from db using Domain-Models
            var regionDomain= await regionRepository.GetByIdAsync(id);
            if(regionDomain==null){
                return NotFound();
            }
            //map domain-model to dto
            var regionDto=new RegionDto{
                Id=regionDomain.Id,
                Code=regionDomain.Code,
                Name=regionDomain.Name,
                RegionImageUrl=regionDomain.RegionImageUrl
            };
            //returning DTO
            return Ok(regionDto);
        }

        // GET REGIONS BY CODE          #3
        [HttpGet]
        [Route("code/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute]String code){
            var regions= await regionRepository.GetByCodeAsync(code);
            if(!regions.Any()){
                return NotFound();
            }
            
            return Ok(regions);
        }

        // GET REGIONS BY NAME          #4
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetByName([FromRoute]string name){
            var regions=await regionRepository.GetByNameAsync(name);
            if(!regions.Any()){
                return NotFound();
            }
            return Ok(regions);
        }

        // POST NEW REGION
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionCreateRequestDto addRegionCreateRequestDto){
            //mapping DTO to Domain-Model
            var regionDomainModel=new Region{
                Code=addRegionCreateRequestDto.Code,
                Name=addRegionCreateRequestDto.Name,
                RegionImageUrl=addRegionCreateRequestDto.RegionImageUrl,
            };

            //using domain-model to insert data into db
            regionDomainModel=await regionRepository.CreateRegionAsync(regionDomainModel);

            //Map Domain-Model back to dto to send back to client
            var regionDto=new RegionDto{
                Id=regionDomainModel.Id,
                Code=regionDomainModel.Code,
                Name=regionDomainModel.Name,
                RegionImageUrl=regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById),new {id=regionDomainModel.Id},regionDomainModel);
        }

        //DELETE
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute]Guid id){
            var regionDomainModel=await regionRepository.GetByIdAsync(id);
            if(regionDomainModel is null){
                return NotFound();
            }
            //Remove has no async Version
            await regionRepository.DeleteRegionAsync(id);

            var regionDto=new RegionDto{
                Id=regionDomainModel.Id,
                Code=regionDomainModel.Code,
                Name=regionDomainModel.Name,
                RegionImageUrl=regionDomainModel.RegionImageUrl
            };


            return Ok(regionDto);
        }
    }
}