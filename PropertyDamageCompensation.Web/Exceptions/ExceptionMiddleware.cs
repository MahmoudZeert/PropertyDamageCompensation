using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace PropertyDamageCompensation.Web.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
       // private readonly IUrlHelperFactory _urlHelper;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public  async  Task InvokeAsync(HttpContext context, LinkGenerator linkGenerator)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex )
            {

                await HandleException(context,ex, linkGenerator);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex, LinkGenerator linkGenerator)
        {
            string title = "";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string errorMessage = "An error occurred while processing your request.";
            if (ex is BadRequestException || (ex is HttpRequestException && (((HttpRequestException)ex).StatusCode== HttpStatusCode.BadRequest)))
            {
                title = "Bad Request";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest ;
                errorMessage = ex.Message;
            }
            else if (ex is NotFoundException)
            {
                title = "Not Found";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorMessage = ex.Message;
            }
            else if (ex is DbUpdateConcurrencyException)
            {

                title = "Resource no longer exists";
                errorMessage = " The resource you're trying to update or delete is no longer exists in the DB !! ";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                // TODO: Save the exception to a database or send an email notification
            }
            else if (ex is DbUpdateException)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("unique index "))
                {
                    title = "Duplicate Name";
                    errorMessage="  Name already exists !! ";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else if (ex.InnerException != null && ex.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    title = "Forbidden  ";
                    errorMessage = "Unable to delete this resource !!." ;
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                }
                else
                {
                    title = "Internal server Error";
                    errorMessage = "A database error occurred while processing your request.---"+ex.InnerException?.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                // TODO: Save the exception to a database or send an email notification
            }

            else if (ex is ForbiddenException)
            {
                title = "Access Denied";
                context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                errorMessage = ex.Message;
            }
            else
            {
                title = "Internal Server Error" + ex.GetType().Name;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorMessage = ex.Message;
            }
            var errorObject = new
            {
                Title = title,
                StatusCode = context.Response.StatusCode,
                ErrorMessage = errorMessage
            };
            var jsonErroObject = JsonSerializer.Serialize(errorObject);
            // Redirect to the error action method with error message
           // var urlHelper = urlHelperFactory.GetUrlHelper(urIActionContext);
            var errorUrl = linkGenerator.GetUriByAction(context,"MyError","Home",new {area="",error= jsonErroObject });
             context.Response.Redirect(errorUrl);
            return Task.CompletedTask;
        }
    }
}
