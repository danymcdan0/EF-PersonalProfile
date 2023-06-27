using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalProfileUI.Data;
namespace PersonalProfileUI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
   var connectionString = builder.Configuration.GetConnectionString("PersonalProfileUIContextConnection") ?? throw new InvalidOperationException("Connection string 'PersonalProfileUIContextConnection' not found.");

   builder.Services.AddDbContext<PersonalProfileUIContext>(options => options.UseSqlServer(connectionString));

   builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<PersonalProfileUIContext>();

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddHttpClient();

			var app = builder.Build();

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

			app.Run();
		}
	}
}