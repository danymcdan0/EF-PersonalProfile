using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace PersonalProfileUI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddHttpClient();

			//Adding cookie Auth: 
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
				options => {
					//Redirect UnAuthenticated users here
					options.LoginPath = "/Auth/Index";
					//Choose name of the auth cookie
					options.Cookie.Name = "PP_auth_cookie";
					//options.AccessDeniedPath = "/Auth/AccessDenied";
				});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles(new StaticFileOptions {
				FileProvider= new PhysicalFileProvider(Path.Combine(
					Directory.GetCurrentDirectory(), "Images")),
				RequestPath="/Images"
			});

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}