using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TaxCalculator.API.Shared.Models;

namespace TaxCalculator.API.Middlewares;

/// <summary>
/// This handler will catch any validation errors within the endpoints that was missed on the front-end
/// </summary>
/// <typeparam name="T"></typeparam>
public class EndpointValidationHandler<T> : ObjectResult
{
    public EndpointValidationHandler(ActionContext context) : base(new GenericResponseModel<T?>
    {
        Success = false,
        Data = default
    })
    {
        ConstructErrorMessages(context);
        StatusCode = 500;
    }

    /// <summary>
    /// Constructing the error messages from the validation context
    /// </summary>
    /// <param name="context">The action context</param>
    private void ConstructErrorMessages(ActionContext context)
    {
        var responseModel = Value as GenericResponseModel<T>;

        foreach (var (_, value) in context.ModelState)
        {
            var errors = value.Errors;
            var errorMessages = new string[errors.Count];
            for (var i = 0; i < errors.Count; i++)
            {
                errorMessages[i] = GetErrorMessage(errors[i]);
            }
            responseModel!.Message = string.Join(", ", errorMessages);
        }
    }
    
    /// <summary>
    /// Retrieving plain error message
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    private static string GetErrorMessage(ModelError error)
    {
        return error.ErrorMessage;
    }
}