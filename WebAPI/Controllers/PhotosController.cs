using AutoMapper;
using Business.Abstarct;
using CloudinaryDotNet;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Helpers;
using Core.Extensions;
using CloudinaryDotNet.Actions;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/cities/{cityid}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private IPhotoService _photoService;
        private ICityService _cityService;
        private IMapper _mapper;
        private IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(IPhotoService photoService,ICityService cityService,IMapper mapper,IOptions<CloudinarySettings> options)
        {
            _photoService = photoService;
            _cityService = cityService;
            _mapper = mapper;
            _cloudinaryConfig = options;

            Account account =
                new Account(_cloudinaryConfig.Value.CloudName,_cloudinaryConfig.Value.ApiKey,_cloudinaryConfig.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }

        [HttpPost]
        public IActionResult AddPhotosForCity(int cityid ,[FromForm]PhotoForCreationDto photoForCreationDto)
        {
            var city = _cityService.GetCityById(cityid);
            if(city.Data==null)
            {
                return BadRequest("Cloud not find the city");
            }

            //var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUserId = Convert.ToInt32(User.ClaimIdentifier());

            if(currentUserId!=city.Data.UserId)
            {
                return Unauthorized();
            }

            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if(file.Length>0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Url.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);
            photo.City = city.Data;

            if(!city.Data.photos.Any(p=>p.IsMain))
            {
                photo.IsMain = true;
            }

            var result = _photoService.Add(photo);

            if(result.Success)
            {
                var photoFromDb = _photoService.GetPhotoById(photo.Id);
                return Ok(photoFromDb);
            }

            return BadRequest("Could not add photo");

        }

    }
}
