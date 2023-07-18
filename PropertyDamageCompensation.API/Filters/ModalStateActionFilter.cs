using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
namespace PropertyDamageCompensation.API.Filters
{
    public class ValidationActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // Get the validation errors from the ModelState
                var validationErrors = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // Create a custom response object
                var customResponse = new
                {
                    Title = "Validation Errvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvor",
                    StatusCode = 400,
                    ErrorMessage = "One or more validation errors occurred.",
                    Errors = validationErrors
                };

                // Create a new JSON result with the custom response
                var jsonResult = new JsonResult(customResponse)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };

                // Set the result on the action context
                context.Result = jsonResult;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Not used in this example
        }
    }
}