using AutoMapper;
using Business.Abstarct;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Core.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private ICityService _cityService;
        private IPhotoService _photoService;
        private IMapper _mapper;
        public CitiesController(ICityService cityService, IPhotoService photoService, IMapper mapper)
        {
            _cityService = cityService;
            _photoService = photoService;
            _mapper = mapper;
        }

        [HttpGet("getcities")]
        public IActionResult GetCities()
        {
            var result = _cityService.GetCities();
            if (result.Success)
            {
                var citiesToReturn = _mapper.Map<List<CityForListDto>>(result.Data);
                return Ok(citiesToReturn);
            }
            return NotFound();
        }

        [HttpGet("detail")]
        public IActionResult GetCitiesById(int cityId)
        {
            var result = _cityService.GetCityById(cityId);
            if (result.Success)
            {
                var citiesToReturn = _mapper.Map<CityForDetailDto>(result.Data);
                return Ok(citiesToReturn);
            }
            return NotFound();
        }

        [HttpPost("add")]
        public IActionResult Add(City city)
        {
            city.UserId = Convert.ToInt32(User.ClaimIdentifier());
            var result = _cityService.Add(city);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest();
        }

        [HttpGet("getphotosbycity")]
        public IActionResult GetPhotosByCity(int cityId)
        {
            var result = _photoService.GetPhotosByCity(cityId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest();
        }



    }
}
