using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionAttribute;
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
            var regionsDomain=await regionRepository.GetAllAsync();
            var regionDto=mapper.Map<List<RegionDto>>(regionsDomain);
            //return DTO
            return Ok(regionDto);
        }

        // GET REGION BY ID         #2
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id){
            var regionDomain= await regionRepository.GetByIdAsync(id);
            if(regionDomain==null){
                return NotFound();
            }

            var regionDto=mapper.Map<RegionDto>(regionDomain);
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
            
            return Ok(mapper.Map<List<RegionDto>>(regions));
        }

        // GET REGIONS BY NAME          #4
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetByName([FromRoute]string name){
            var regions=await regionRepository.GetByNameAsync(name);
            if(!regions.Any()){
                return NotFound();
            }

            return Ok(mapper.Map<List<RegionDto>>(regions));
        }

        // POST NEW REGION
        [HttpPost]
        [ModelValidation]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionCreateRequestDto addRegionCreateRequestDto){
            var regionDomainModel=mapper.Map<Region>(addRegionCreateRequestDto);
            regionDomainModel=await regionRepository.CreateRegionAsync(regionDomainModel);

            var regionDto=mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById),new {id=regionDomainModel.Id},regionDomainModel);
        }

        //Update existing Region
        [HttpPut]
        [Route("{id:Guid}")]
        [ModelValidation]
        public async Task<IActionResult> UpdateById([FromRoute]Guid id, [FromBody]UpdateRegionRequestDto updateRegionRequestDto){
            var regionDomainModel=mapper.Map<Region>(updateRegionRequestDto);
            regionDomainModel=await regionRepository.UpdateByIdAsync(id,regionDomainModel);
            if(regionDomainModel is null) return NotFound();

            return Ok(mapper.Map<RegionDto>(regionDomainModel)); 
        }

        //DELETE
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute]Guid id){
            var regionDomainModel=await regionRepository.GetByIdAsync(id);
            if(regionDomainModel is null){
                return NotFound();
            }

            await regionRepository.DeleteRegionAsync(id);
            var regionDto=mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }
    }
}