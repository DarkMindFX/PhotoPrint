using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Text.Json;

namespace PPT.PhotoPrint.API.Filters
{
    public class UnhandledExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var error = new DTO.Error();
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError; 

            if(context.Exception is SqlException)
            {
                var exception = context.Exception as SqlException;
                statusCode = HttpStatusCode.BadRequest;
                error.Message = exception.Message;
            }
            else if(context.Exception is PPT.Services.Common.Exceptions.UnauthorizedException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                error.Message = $"Access Denied: {context.Exception.Message}";
            }
            else
            {                
                error.Message = context.Exception.GetBaseException().Message;
            }

            error.Code = (int)statusCode;

            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.Result = new ObjectResult(new { statusCode = statusCode, currentDate = DateTime.Now, response = JsonSerializer.Serialize(error) });

            base.OnException(context);
        }
    }
}
