using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PropertyDamageCompensation.Web.Areas.Compensation.Services;
using PropertyDamageCompensation.Web.Areas.Identity.Authorization;
using PropertyDamageCompensation.Web.Data;
using PropertyDamageCompensation.Web.Email;
using PropertyDamageCompensation.Web.Email.EMailService;
using PropertyDamageCompensation.Web.Exceptions;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(
    options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAllHttpClients();
builder.Services.AddControllersWithViews();
builder.Services.AddRouting();

builder.Services.AddMvc()
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.ClientValidationEnabled = true;
    });
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events = new CookieAuthenticationEvents
    {
        OnRedirectToAccessDenied = context =>
        {
            throw new ForbiddenException("You don't have permission to access this resource !");
        }
        
    };
});
builder.Services.AddTransient<IEmailSending,EmailSenddinfService>();
builder.Services.AddAuthorizationPDC();
var app = builder.Build();

var supportedCultures = new List<CultureInfo>
{
    new CultureInfo("en-Us"),
    new CultureInfo("fr-FR")
};
    
app.UseRequestLocalization(new RequestLocalizationOptions
                           {
    DefaultRequestCulture=new RequestCulture("fr-FR"),
    SupportedCultures=supportedCultures,
    SupportedUICultures=supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>() {  }
});

//app.UseExceptionMiddleware();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    
}
else
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseExceptionMiddleware();
app.UseStatusCodePages(async context =>
{   
    var response = context.HttpContext.Response;
    if (response.StatusCode == StatusCodes.Status404NotFound)
    {
        throw new NotFoundException("The resource you're locating for does not exists !!");
    };
    
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
