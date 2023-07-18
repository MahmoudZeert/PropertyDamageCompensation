using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PropertyDamageCompensation.API.Filters
{
    public class DataBindingExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if ( context.Exception is ValidationException)
            {
                // Handle the model binding/validation exception here
                context.Result = new BadRequestObjectResult("Invalid request data.");
                context.ExceptionHandled = true;
            }
        }
    }
}

