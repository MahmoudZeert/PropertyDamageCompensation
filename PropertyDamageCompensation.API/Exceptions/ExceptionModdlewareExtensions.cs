namespace PropertyDamageCompensation.API.Exceptions
{
    public static class ExceptionModdlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
