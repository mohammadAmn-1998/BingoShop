using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Users1.WebUI.Bootstrapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("default");
services.AddHttpContextAccessor();
services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(opt =>
{
	opt.Cookie.Name = "Authentication.Cookie";
	opt.LoginPath = "/Account";
	opt.LogoutPath = "/Account/Logout";
	opt.ExpireTimeSpan = TimeSpan.FromDays(5);


});
services.AddAuthorization();
DependencyBootstrapper.Config(services,connectionString!);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
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

	endpoints.MapControllerRoute(
		name: "Admin",
		pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);
});
app.Run();
