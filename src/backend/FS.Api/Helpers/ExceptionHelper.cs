namespace FS.Api.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    public static class ExceptionHandler
    {
        private static readonly JsonSerializerSettings jsonSerializerSettings;
        private static readonly string contentType;

        static ExceptionHandler()
        {
            jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            contentType = "application/json";
        }

        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(
                errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        var errors = HandleException(context);

                        context.Response.ContentType = contentType;

                        await context.Response.WriteAsync(GetJson(errors)).ConfigureAwait(true);
                    });
                });
        }

        private static ApplicationErrorCollection HandleException(HttpContext context)
        {
            var exception = context.Features.Get<IExceptionHandlerFeature>().Error;

            var errors = new List<ApplicationError>();

            switch (exception)
            {
                case ArgumentNullException argumentNullException:
                    HandleBadRequest(context, argumentNullException, errors);
                    break;

                case ArgumentException argumentException:
                    HandleBadRequest(context, argumentException, errors);
                    break;

                // case NotFoundException notFoundException:
                //     HandleResourceNotFound(context, notFoundException, errors);
                //     break;
                // case UnauthorizedException unauthorizedException:
                //     HandleUnauthorizedException(context, unauthorizedException, errors);
                //     break;
                default:
                    HandleUnknownError(context, exception, errors);
                    break;
            }

            return new ApplicationErrorCollection
            {
                Errors = errors
            };
        }

        private static void HandleUnauthorizedException(HttpContext context, Exception exception, List<ApplicationError> errors)
        {
            errors.Add(new ApplicationError
            {
                Code = (int)HttpStatusCode.Unauthorized,
                Message = exception.Message,
                Exception = null
            });

            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
        }

        private static void HandleBadRequest(HttpContext context, Exception exception, List<ApplicationError> errors)
        {
           
            errors.Add(new ApplicationError
            {
                Code = (int)HttpStatusCode.BadRequest,
                Message = exception.Message,
                Exception = null
            });

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }

        private static void HandleResourceNotFound(HttpContext context, Exception exception, List<ApplicationError> errors)
        {
            errors.Add(new ApplicationError
            {
                Code = (int)HttpStatusCode.NotFound,
                Message = exception.Message,
                Exception = null
            });

            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }

        private static void HandleUnknownError(HttpContext context, Exception exception, List<ApplicationError> errors)
        {
            errors.Add(
               new ApplicationError
               {
                   Code = (int)HttpStatusCode.InternalServerError,
                   Message = "Error during execution",
                   Exception = exception
               });
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        private static string GetJson(ApplicationErrorCollection errors)
        {
            return JsonConvert.SerializeObject(
                            errors,
                            Formatting.None,
                            jsonSerializerSettings);
        }
    }

    internal class ApplicationErrorCollection
    {
        public List<ApplicationError> Errors { get; set; }
    }

    internal class ApplicationError
    {
        public object Code { get; set; }
        public string Message { get; set; }
        public object Exception { get; set; }
    }
}