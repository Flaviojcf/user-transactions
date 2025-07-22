using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using UserTransactions.Communication.Dtos.Errors.Response;
using UserTransactions.Exception;
using UserTransactions.Exception.Exceptions;

namespace UserTransactions.API.Filter
{
    [ExcludeFromCodeCoverage]
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is UserTransactionsException)
            {
                HandleProjectException(context);

                return;
            }

            ThrowUnknownException(context);
        }

        private static void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationException errorOnValidationException)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                context.Result = new BadRequestObjectResult(new ResponseErrorDto(errorOnValidationException!.Errors, HttpStatusCode.BadRequest));

                return;
            }

            if (context.Exception is DomainException domainException)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                context.Result = new BadRequestObjectResult(new ResponseErrorDto(domainException.Message, HttpStatusCode.BadRequest));

                return;
            }

            if (context.Exception is ErrorOnAuthorizeException errorOnAuthorizeException)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Result = new ObjectResult(new ResponseErrorDto(errorOnAuthorizeException!.Errors, HttpStatusCode.Forbidden))
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }
        }

        private static void ThrowUnknownException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorDto(ResourceMessagesException.UnknownError, HttpStatusCode.InternalServerError));
        }
    }
}
