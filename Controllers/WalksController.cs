using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionAttribute;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        [HttpPost]
        [ModelValidation]
        public async Task<IActionResult> Create([FromBody] AddWalksCreateDto addWalksCreateDto){
                var walkDomainModel=mapper.Map<Walk>(addWalksCreateDto);

            await walkRepository.CreateAsync(walkDomainModel);

            return Ok(mapper.Map<WalkDto>(walkDomainModel));   
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var walkDomain=await walkRepository.GetAllAsync();

            var walkDto=mapper.Map<List<WalkDto>>(walkDomain);
            return Ok(walkDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id){
            var walkDomain=await walkRepository.GetByIdAsync(id);
            if(walkDomain is null){
                return NotFound();
            }

            var walkDto=mapper.Map<WalkDto>(walkDomain);
            return Ok(walkDto);
        }
    
        [HttpPut]
        [Route("{id:Guid}")]
        [ModelValidation]
        public async Task<IActionResult> UpdateById([FromRoute]Guid id, [FromBody]UpdateWalkRequestDto updateWalkRequestDto){
            var walkDomainModel=mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel=await walkRepository.UpdateByIdAsync(id,walkDomainModel);
            if(walkDomainModel is null) return NotFound();

            var walkDto=mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDomainModel);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute]Guid id){
            var walk= await walkRepository.DeleteByIdAsync(id);
            if(walk is null) return NotFound();

            return Ok(mapper.Map<WalkDto>(walk));
        }
    }
}