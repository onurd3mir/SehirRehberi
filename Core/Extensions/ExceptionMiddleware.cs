using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext,e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            string message = "Internel Server Error";
            httpContext.Response.ContentType = "application/json";

            if (e.GetType()==typeof(ValidationException))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                message = e.Message;
            }
            else if(e.GetType() == typeof(AuthorizeException))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                message = e.Message;
            }
            else
            {     
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString()) ;
        }
    }
}
