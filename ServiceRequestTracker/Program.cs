using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using ServiceRequestTracker.Models;
using ServiceRequestTracker.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();

builder.Services.AddDbContext<ServiceTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServiceDB")));

var app = builder.Build();

RotativaConfiguration.Setup(app.Environment.WebRootPath);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ServiceRequests}/{action=Create}/{id?}");

app.Run();

