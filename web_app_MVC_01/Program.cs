using Microsoft.EntityFrameworkCore;
using web_app_MVC_01.Data;
using web_app_MVC_01.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SchoolContext>
    (x => x.UseSqlServer(builder.Configuration.GetConnectionString("SAConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var sv = scope.ServiceProvider;
    DbInitializer.Initialize(sv);
}

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(name: "/",
    pattern:
    "/",
    defaults: new { controller = "Student", action = "Index" });

app.Run();