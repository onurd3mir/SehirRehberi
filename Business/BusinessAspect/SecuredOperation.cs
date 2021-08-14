using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.BusinessAspect
{
    public class SecuredOperation:MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string role=null)
        {
            if(!string.IsNullOrEmpty(role)) _roles = role.Split(',');

            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {

            if(!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new AuthorizeException(AspectMessages.AuthorizationDenid);
            }

            if(_roles.Length>0)
            {
                var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
                foreach (var role in _roles)
                {
                    if (roleClaims.Contains(role))
                    {
                        return;
                    }
                }
            }

            throw new AuthorizeException(AspectMessages.AuthorizationDenid);
        }
    }
}
