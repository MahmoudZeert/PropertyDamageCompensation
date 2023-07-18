namespace PropertyDamageCompensation.Web.Areas.Identity.Authorization
{
    public static class AuthorizationExtensions
    {
        public static void AddAuthorizationPDC(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin",
                    policy => policy.RequireRole( "Admin"));
                options.AddPolicy("IsApplicant",
                    policy => policy.RequireAssertion(context=>
                    context.User.IsInRole("Applicant")||
                    context.User.IsInRole("Admin")));
                options.AddPolicy("IsEngineer",
                    policy => policy.RequireRole("Employee", "Engineer"));
                options.AddPolicy("IsChiefEngineer",
                    policy => policy.RequireAssertion(context =>
                    context.User.IsInRole("Chief Engineer") ||
                    context.User.IsInRole("Admin")));
            });
        }
    }
}
