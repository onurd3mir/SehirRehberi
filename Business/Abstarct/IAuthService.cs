using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstarct
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForLoginDto userForLoginDto);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IDataResult<User> UserExists(string username);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
