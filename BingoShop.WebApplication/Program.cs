using BingoShop.WebApplication.Services;
using Blogs.Application.Bootstrapper;
using Blogs.Infrastructure.Bootstrapper;
using Shared.Application.Services;
using Users.Infrastructure.Bootstrapper;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("local");

#region Ioc

BlogInfraBootstrapper.Config(services, connectionString!);
UsersInfraBootstrapper.Config(services, connectionString!);
BlogServiceBootstrapper.Config(services);

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
