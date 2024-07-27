using BingoShop.WebApplication.Services;
using Blogs.Application.Bootstrapper;
using Blogs.Infrastructure.Bootstrapper;
using Shared.Application.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("local");

#region Ioc

InfraBootstrapper.InstallBlogServices(services, connectionString!);
ServiceBootstrapper.InstallBlogServices(services);
services.AddTransient<IFileService, FileService>();

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
