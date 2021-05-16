using ApplicationL.DTOs;
using AutoMapper;
using DomainL.Models;
using InfrastructureL.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoGhana.Controllers
{
    /// <summary>
    /// Localities in GeoGhana API
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LocalitiesController : Controller
    {
        public readonly ILocality _service;
        private readonly IMapper _mapper;

        public LocalitiesController(ILocality repository, IMapper mapper)
        {
            _service = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets list of all Localities in GeoGhana API
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<LocalityView>> Get()
        {
            var result = _service.GetAllLocalities();

            return Ok(_mapper.Map<IEnumerable<LocalityView>>(result));
        }

        /// <summary>
        /// Find a Locality in Ghana using postcode
        /// </summary>
        [HttpGet]
        [Route("Postcode")]
        public async Task<ActionResult<LocalityView>> FindByPostCode([FromQuery(Name = "code")] string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return BadRequest();
            }

               var result = await _service.SearchPostCode(code);
                return Ok(_mapper.Map<LocalityView>(result));
                        
        }

        /// <summary>
        /// Search Locality by name
        /// </summary>
        [HttpGet("{localityName}", Name = "SearchLocality")]
        public async Task<ActionResult<LocalityView>> SearchLocalityByName(string localityName)
        {
            var request = await _service.SearchLocalityByName(localityName);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<LocalityView>(request));
        }

        /// <summary>
        /// Query for Localities using all or part of name
        /// </summary>
        [HttpGet]
        [Route("Query")]
        public async Task<ActionResult<IEnumerable<LocalityView>>> GetAllLocalities(
            [FromQuery(Name = "name")]string name)
        {
            var request = await _service.QueryLocalityName(name);
            if (!request.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<LocalityView>>(request));
        }

        /// <summary>
        /// Add new Locality to GeoGhana API
        /// </summary>
        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult<LocalityAdd>> AddLocalityInfo(
            [FromBody] LocalityAdd localityToAdd)
        {
            var duplicates = await _service.QueryLocalityName(localityToAdd.Name);
            if (!duplicates.Any())
            {
                var localityModel = _mapper.Map<Locality>(localityToAdd);

                _service.AddNewLocality(localityModel);
                _service.SaveChanges();

                 return Ok();
            }
            else
            {
                foreach (var item in duplicates)
                {
                   if (item.City.Name == localityToAdd.CityName)
                    {
                        throw new ApplicationException("Duplicate locality in the same city found.");
                    }
                }
                    return BadRequest($"{localityToAdd.CityName} already contains {localityToAdd.Name}.");
            }
            
        }

        /// <summary>
        /// Insert Locality to GeoGhana API
        /// </summary>
        [HttpPut("{localityCode}")]
        public async Task<ActionResult> UpdateLocalityInfo(string localityCode, LocalityUpdate localityToUpdate)
        {
            var localityModel = await _service.SearchLocalityByName(localityCode);
            if (localityModel == null)
            {
                return NotFound();
            }

            _mapper.Map(localityToUpdate, localityModel);

            _service.UpdateLocality(localityModel);
            _service.SaveChanges();

            return Ok(_service.SearchLocalityByName(localityCode));
        }

        /// <summary>
        /// Modify part of Locality information
        /// </summary>
        [HttpPatch("{localityCode}")]
        public async Task<ActionResult> PartialUpdatelocality(string localityCode, JsonPatchDocument<LocalityUpdate> patchDoc)
        {
            var localityModel = await _service.SearchLocalityByName(localityCode);
            if (localityModel == null)
            {
                return NotFound();
            }

            var localityToPatch = _mapper.Map<LocalityUpdate>(localityModel);
            patchDoc.ApplyTo(localityToPatch, ModelState);

            if (!TryValidateModel(localityToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(localityToPatch, localityModel);
            _service.UpdateLocality(localityModel);
            _service.SaveChanges();

            return Ok(_service.SearchLocalityByName(localityCode));
        }

        /// <summary>
        /// Remove Locality from GeoGhana API.
        /// </summary>
        [HttpDelete("{postCode}")]
        public async Task<ActionResult> RemoveLocality(string postcode)
        {
            var request = await _service.SearchPostCode(postcode);
            if (request == null)
            {
                return BadRequest();
            }
                _service.DeleteLocality(request);
                _service.SaveChanges();
                return NoContent();
        }
    }
}
