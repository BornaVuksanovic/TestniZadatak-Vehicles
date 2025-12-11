using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Project.Data;
using Project.MVC.MappingProfiles;
using Project.Service.Interfaces;
using Project.Service.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VehicleDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("VehicleDb")));

builder.Services.AddScoped<IVehicleService, VehicleService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<VehicleProfile>();
});
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
