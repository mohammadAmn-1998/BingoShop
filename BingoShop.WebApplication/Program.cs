using BingoShop.WebApplication.DependencyBootstrapper;
using BingoShop.WebApplication.Services;
using Blogs.Application.Bootstrapper;
using Blogs.Infrastructure.Bootstrapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Shared.Application.Services;
using Users.Infrastructure.Bootstrapper;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllersWithViews();

services.AddHttpContextAccessor();

var connectionString = builder.Configuration.GetConnectionString("local");

#region Auth

services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(options =>
{
	options.Cookie.Name = "Authentication.Cookie";
	options.ExpireTimeSpan = TimeSpan.FromDays(30);
	options.LoginPath = "/Home/Login";
	options.LogoutPath = "/Home/Logout";
	options.AccessDeniedPath = "/Home/not-permitted";
});

#endregion

#region Dependency 

Moduls_Bootstrapper.Config(services,connectionString!);

services.AddTransient<IFileService, BingoShop.WebApplication.Services.FileService>();
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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


	endpoints.MapAreaControllerRoute(
	  name: "Admin",
	  pattern: "Admin/{controller=Home}/{action=Index}/{id?}",
	  areaName: "Admin"
	  
	);
});


app.Run();
