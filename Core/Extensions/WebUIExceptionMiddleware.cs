using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class WebUIExceptionMiddleware
    {
        private RequestDelegate _next;
        public WebUIExceptionMiddleware(RequestDelegate next)
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
                //await HandleExceptionAsync(httpContext, e);

                string message = "Internel Server Error";
                //httpContext.Response.ContentType = "application/json";

                if (e.GetType() == typeof(ValidationException))
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    message = e.Message;
                }
                else if (e.GetType() == typeof(AuthorizeException))
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    message = e.Message;

                    httpContext.Response.Redirect("/auths/AccessDenied");
                }
                else
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    httpContext.Response.Redirect("/Home/Error");
                }
            }
        }

        
    }
}
