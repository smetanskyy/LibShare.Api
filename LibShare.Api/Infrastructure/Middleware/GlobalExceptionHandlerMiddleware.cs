using LibShare.Api.Data.ApiModels.ResponseApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Resources;
using System.Threading.Tasks;
using System.Web.Http;

namespace LibShare.Api.Infrastructure.Middleware
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }


    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ResourceManager _resourceManager;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ResourceManager resourceManager)
        {
            _next = next;
            _resourceManager = resourceManager;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpResponseException)
            {
                throw;
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json; charset=utf-8";

                var desctiption = new MessageApiModel(error.Message);

                if (error is BadImageFormatException)
                    desctiption.Message = _resourceManager.GetString("BadImageFormatException");
                else if (error is FormatException)
                    desctiption.Message = _resourceManager.GetString("FormatException");
                else if (error is ArgumentNullException)
                    desctiption.Message = _resourceManager.GetString("ArgumentNullException");

                var details = new ErrorDetails
                {
                    ErrorId = Guid.NewGuid().ToString(),
                    RequestPath = context.Request.Path.Value,
                    EndpointPath = context.GetEndpoint()?.ToString(),
                    TimeStamp = DateTime.Now,
                    Message = desctiption.Message
                };

                switch (error)
                {
                    case DirectoryNotFoundException _:
                    case FileNotFoundException _:
                    case InvalidOperationException _:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    case KeyNotFoundException _:
                    case NotFoundException _:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ArgumentNullException _:
                    case ArgumentOutOfRangeException _:
                    case BadImageFormatException _:
                    case ArgumentException _:
                    case FormatException _:
                    case SecurityTokenException _:
                    case BadRequestException _:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var jsonOptions = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented
                };

                var result = response.StatusCode == 500
                    ? JsonConvert.SerializeObject(details, jsonOptions)
                    : JsonConvert.SerializeObject(desctiption, jsonOptions);
                await response.WriteAsync(result);
            }
        }
    }
}