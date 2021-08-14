using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;

namespace Core.Aspect.Autofac.Logging
{
    public class LogAspect:MethodInterception
    {
        private LoggerServiceBase _LoggerServiceBase;
        private IHttpContextAccessor _httpContextAccessor;

        public LogAspect(Type loggerService)
        {
            if(loggerService.BaseType!=typeof(LoggerServiceBase))
            {
                throw new System.Exception(AspectMessages.WrongLoggerType);
            }
            _LoggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _LoggerServiceBase.Info(GetLogDetail(invocation));
        }

        //protected override void OnAfter(IInvocation invocation)
        //{
        //    _LoggerServiceBase.Error(GetLogDetail(invocation));
        //}

        private LogDetailWithUser GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name=invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            var logDetail = new LogDetailWithUser
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters,
                UserId = _httpContextAccessor.HttpContext.User.ClaimIdentifier()
            };

            return logDetail;
        }
    }
}
