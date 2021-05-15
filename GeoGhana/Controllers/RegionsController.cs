using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationL;
using InfrastructureL.Interfaces;
using ApplicationL.DTOs;
using AutoMapper;
using DomainL.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.JsonPatch;

namespace GeoGhana.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly IRegion _service;
        private readonly IMapper _mapper;

        public RegionsController(IRegion repository, IMapper mapper)
        {
            _service = repository;
            _mapper = mapper;
        }

        #region Get Methods
        [HttpGet]
        // GET: api/Regions
        public async Task<ActionResult<IEnumerable<RegionView>>> Get()
        {
            var result = await _service.GetAllRegions();
            return Ok(_mapper.Map<IEnumerable<RegionView>>(result));
        }

        /// <summary>
        /// Gets full details of Regions
        /// </summary>
        /// <returns>List of detailed Regions</returns>
        [HttpGet("Complete")]
        public async Task<ActionResult<IEnumerable<RegionFull>>> GetRegionsComplete()
        {
            var result = await _service.GetAllRegions();
            return Ok(_mapper.Map<IEnumerable<RegionFull>>(result));
        }

        /// <summary>
        /// Search Region By Code
        /// </summary>
        /// <returns>Region</returns>
        [HttpGet("SearchByCode",Name = "SearchByRegCode")]
        public async Task<ActionResult<RegionFull>> SearchByRegCodeAsync([FromQuery(Name = "code")] string regionCode)
        {
            var request = await _service.SearchRegionByCode(regionCode);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RegionFull>(request));
        }
        [HttpGet]
        [Route("Query")]
        public async Task<ActionResult<IEnumerable<RegionView>>> SearchRegionLike([FromQuery(Name = "name")] string name)
        {
            var request = await _service.QueryRegionName(name);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<RegionView>>(request));
        }

        #endregion

        [HttpPost("Add")]
        public async Task<ActionResult<RegionAdd>> AddRegionInfo([FromBody] RegionAdd regionToAdd)
        {
            var regModel = _mapper.Map<Region>(regionToAdd);
            var duplicate = await _service.QueryRegionName(regionToAdd.Name);
            if (!duplicate.Any())
            {
                try
                {
                    _service.AddNewRegion(regModel);
                    _service.SaveChanges();
                }
                catch (Exception)
                {
                    return BadRequest("Duplicate RegionCode detected.");
                }

                var regReadDto = _mapper.Map<RegionView>(regModel);

                return CreatedAtRoute(
                                        "SearchByRegCode",
                                        new { regionCode = regReadDto.RegionCode },
                                        regReadDto
                                        );
            }
            else
            {
                return BadRequest($"{regionToAdd.Name} already exists.");
            }
        }

        [HttpPut("{regionCode}")]
        public async Task<ActionResult> UpdateRegionInfoAsync(string regionCode, RegionUpdate regionToUpdate)
        {
            var regModel = await _service.SearchRegionByCode(regionCode);
            if (regModel == null)
            {
                return NotFound();
            }
            _mapper.Map(regionToUpdate, regModel);

            _service.UpdateRegion(regModel);
            _service.SaveChanges();

            return Ok(_service.SearchRegionByCode(regionCode));
        }

        [HttpPatch("{regionCode}")]
        public async Task<ActionResult> PartialUpdateRegionAsync(string regionCode, JsonPatchDocument<RegionUpdate> patchDoc)
        {
            var regionModel = await _service.SearchRegionByCode(regionCode);
            if (regionModel == null)
            {
                return NotFound();
            }

            var regionToPatch = _mapper.Map<RegionUpdate>(regionModel);
            patchDoc.ApplyTo(regionToPatch, ModelState);

            if (!TryValidateModel(regionToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(regionToPatch, regionModel);
            _service.UpdateRegion(regionModel);
            _service.SaveChanges();

            return Ok(_service.SearchRegionByCode(regionCode));
        }

        [HttpDelete("{regionCode}")]
        public async Task<ActionResult> DeleteRegionAsync(string regionCode)
        {
            var regModel = await _service.SearchRegionByCode(regionCode);
            if (regModel == null)
            {
                return NotFound();
            }

            _service.DeleteRegion(regModel);
            _service.SaveChanges();

            return NoContent();
        }
    }
}
