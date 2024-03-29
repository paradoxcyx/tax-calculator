﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaxCalculator.API.Shared.Models;
using TaxCalculator.Domain.Exceptions;

namespace TaxCalculator.API.Middlewares;

/// <summary>
/// Globally handle all exceptions in the application and return a generic response model to front-end
/// </summary>
/// <param name="logger"></param>
public class GlobalExceptionHandlerAttribute(ILogger<GlobalExceptionHandlerAttribute> logger) : ExceptionFilterAttribute
{

    public override void OnException(ExceptionContext context)
    {
        var errorMessage = context.Exception.GetType() == typeof(TaxCalculatorException)
            ? context.Exception.Message
            : "Internal Server Error Occurred";
        
        // Handle other exceptions with a generic response
        var errorResponse = new GenericResponseModel<object?>
        {
            Success = false,
            Message = errorMessage,
            Data = null
        };

        context.Result = new ObjectResult(errorResponse)
        {
            StatusCode = 500,
            DeclaredType = typeof(GenericResponseModel<object?>)
        };
        
        logger.LogError(context.Exception.ToString());
    }
}