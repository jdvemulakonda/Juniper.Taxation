using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Juniper.Taxation.Core.Application.Exceptions;
using Juniper.Taxation.Core.Application.Wrappers;
using Microsoft.AspNetCore.Http;

namespace Juniper.Taxation.Middleware
{
    public class ExceptionHandlingMiddleware:IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new Response<string>() { Succeeded = false, Message = exception?.Message };

            switch (exception)
            {
                case ApiException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case ValidationException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Errors = e.Errors;
                    break;
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}
