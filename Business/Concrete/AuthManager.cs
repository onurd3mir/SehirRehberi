using Business.Abstarct;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var cliams = new List<OperationClaim>();
            var token= _tokenHelper.CreateToken(user, cliams);
            return new SuccessDataResult<AccessToken>(token);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = UserExists(userForLoginDto.UserName);
            if (!userToCheck.Success)
            {
                return new ErrorDataResult<User>(userToCheck.Data, "Kullanıcı Bulunamadı");
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(userToCheck.Data, "Paralo Hatalı");
            }

            return new SuccessDataResult<User>(userToCheck.Data);
        }

        public IDataResult<User> Register(UserForLoginDto userForLoginDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForLoginDto.Password,out passwordHash,out passwordSalt);

            var user = new User
            {
                UserName = userForLoginDto.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            _userService.Add(user);

            return new SuccessDataResult<User>(user, "Kayıt Başarılı");
        }

        public IDataResult<User> UserExists(string username)
        {
            var userToCheck = _userService.GetByUsername(username);
            if(userToCheck==null) return new ErrorDataResult<User>(userToCheck,"Kullanıcı Bulunamadı");
            return new SuccessDataResult<User>(userToCheck,"Kullanıcı Mevcut");
        }
    }
}
