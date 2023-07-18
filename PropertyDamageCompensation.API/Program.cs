using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PropertyDamageCompensation.Application.Persistence.FloorService;
using PropertyDamageCompensation.Domain.Interfaces.Persistence;
using PropertyDamageCompensation.Infrastructure.Data;
using PropertyDamageCompensation.Infrastructure.Persistence;
using PropertyDamageCompensation.API.Filters;
using PropertyDamageCompensation.API.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add(typeof(ValidationActionFilter));
//    options.Filters.Add(typeof(DataBindingExceptionFilter));
//});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(
    options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IFloorService, FloorService>();
builder.Services.AddScoped<IFloorRepository, FloorRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionMiddleware();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication(); // Added authentication middleware
app.UseAuthorization();

app.MapControllers();

app.Run();
