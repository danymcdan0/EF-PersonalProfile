using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalProfileAPI.Data;
using PersonalProfileAPI.Mappings;
using PersonalProfileAPI.Models.Domains;
using PersonalProfileAPI.Repository;

namespace PersonalProfileAPI
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

			builder.Services.AddDbContext<PersonalProfileDbContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("PersonalProfileConnectionString")));

			builder.Services.AddIdentityCore<Owner>(options => options.SignIn.RequireConfirmedAccount = false)
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<PersonalProfileDbContext>();

            builder.Services.AddScoped<IEducationRepository, EducationRepository>();
			builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
			builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

			var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedData.Initialise(services);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}