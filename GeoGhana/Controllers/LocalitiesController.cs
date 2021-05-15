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

        [HttpGet]
        public ActionResult<IEnumerable<LocalityView>> Get()
        {
            var result = _service.GetAllLocalities();

            return Ok(_mapper.Map<IEnumerable<LocalityView>>(result));
        }

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
        [HttpGet("{locality}", Name = "SearchLocality")]

        public async Task<ActionResult<LocalityView>> SearchLocalityByName(string locality)
        {
            var request = await _service.SearchLocalityByName(locality);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<LocalityView>(request));
        }

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

        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult<LocalityAdd>> AddLocalityInfo(
            [FromBody] LocalityAdd localityToAdd)
        {
            var duplicates = await _service.QueryLocalityName(localityToAdd.Name);
            if (duplicates.Any())
            {
                foreach (var item in duplicates)
                {

                    if (item.City.Name != localityToAdd.CityName)
                    {
                        var localityModel = _mapper.Map<Locality>(localityToAdd);

                        _service.AddNewLocality(localityModel);
                        _service.SaveChanges();

                        return Ok();
                    } 
                    else if (item.City.Name == localityToAdd.CityName)
                    {
                        return BadRequest($"{localityToAdd.CityName} already contains {localityToAdd.Name}.");
                    }
                }
                return NoContent();
            }
            else
            {
                return Ok();
            }
        }


        [HttpPut("{code}")]
        public async Task<ActionResult> UpdateLocalityInfo(string code, LocalityUpdate localityToUpdate)
        {
            var localityModel = await _service.SearchLocalityByName(code);
            if (localityModel == null)
            {
                return NotFound();
            }

            _mapper.Map(localityToUpdate, localityModel);

            _service.UpdateLocality(localityModel);
            _service.SaveChanges();

            return Ok(_service.SearchLocalityByName(code));
        }

        [HttpPatch("{code}")]
        public async Task<ActionResult> PartialUpdatelocality(string code, JsonPatchDocument<LocalityUpdate> patchDoc)
        {
            var localityModel = await _service.SearchLocalityByName(code);
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

            return Ok(_service.SearchLocalityByName(code));
        }
    }
}
