using BingoShop.WebApplication.DependencyBootstrapper;
using BingoShop.WebApplication.Services;
using Blogs.Application.Bootstrapper;
using Blogs.Infrastructure.Bootstrapper;
using Shared.Application.Services;
using Users.Infrastructure.Bootstrapper;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllersWithViews();

services.AddHttpContextAccessor();

var connectionString = builder.Configuration.GetConnectionString("local");

#region Dependency 

Moduls_Bootstrapper.Config(services,connectionString!);


services.AddTransient<IFileService, FileService>();
services.AddTransient<IAuthService, AuthService>();

#endregion


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
