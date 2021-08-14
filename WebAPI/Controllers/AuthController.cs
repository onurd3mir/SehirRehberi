using Business.Abstarct;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public IActionResult Register(UserForLoginDto userForLoginDto)
        {
            var userExsist = _authService.UserExists(userForLoginDto.UserName);

            if(userExsist.Success)
            {
                return BadRequest(userExsist.Message);
            }

            var registerResult = _authService.Register(userForLoginDto);

            var result = _authService.CreateAccessToken(registerResult.Data);

            if(result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _authService.Login(userForLoginDto);

            if(!userToCheck.Success)
            {
                return BadRequest(userToCheck.Message);
            }

            var result = _authService.CreateAccessToken(userToCheck.Data);

            return Ok(result.Data);
           
        }



    }
}
