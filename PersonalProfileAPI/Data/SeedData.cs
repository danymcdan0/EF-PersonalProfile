using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PersonalProfileAPI.Models.Domains;
using System.Diagnostics.Metrics;

namespace PersonalProfileAPI.Data
{
    public class SeedData
    {
        public static void Initialise(IServiceProvider serviceProvider) 
        {
            var context = serviceProvider.GetRequiredService<PersonalProfileDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Owner>>();
            var roleStore = new RoleStore<IdentityRole>(context);

            #region User SeedData:
            if (context.Owners.Any())
            {
                context.Owners.RemoveRange(context.Owners);
                context.UserRoles.RemoveRange(context.UserRoles);
                context.Roles.RemoveRange(context.Roles);
                context.SaveChanges();
            }

            var owner = new IdentityRole
            {
                Name = "Owner",
                NormalizedName = "OWNER"
            };
            roleStore.CreateAsync(owner).GetAwaiter().GetResult();

            var danyal = new Owner {
                UserName = "danyal-saleh",
                Email = "danyal-1999@hotmail.com",
                EmailConfirmed = true
            };
            userManager.CreateAsync(danyal, "PPPassw0rd?").GetAwaiter().GetResult();

            context.UserRoles.Add(new IdentityUserRole<string> {
                    UserId = userManager.GetUserIdAsync(danyal).Result,
                    RoleId = roleStore.GetRoleIdAsync(owner).Result
             });
            #endregion

            #region PersonalProfile SeedData:
            if (context.Experiences.Any() || context.Education.Any() || context.Projects.Any())
            {
                context.Experiences.RemoveRange(context.Experiences);  
                context.Education.RemoveRange(context.Education);  
                context.Projects.RemoveRange(context.Projects);  
            }

            context.Experiences.AddRange(
                new Experience {
                    Id = Guid.NewGuid(),
                    Company = "SpartaGlobal",
                    Role = "Pre-assignment",
                    Description = "Description",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                },
                new Experience
                {
                    Id = Guid.NewGuid(),
                    Company = "Life",
                    Role = "Existing",
                    Description = "I didnt really get a choice tbh",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                });

            context.Education.AddRange(
                new Education {
                    Id = Guid.NewGuid(),
                    University = "University of Essex",
                    Course = "Bsc Computer Science",
                    Grade = "1:1",
                    Description = "I did 'x' courses",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                },
                new Education
                {
                    Id = Guid.NewGuid(),
                    University = "University of Essex",
                    Course = "Msc Game Development",
                    Grade = "1:2",
                    Description = "I did 'x' courses",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                });

            context.Projects.AddRange(
                new Project {
                    Id = Guid.NewGuid(),
                    Title = "Title of Project",
                    Description = "Description of Project",
                    Aim = "Aim of Project"
                },
                new Project
                {
                    Id = Guid.NewGuid(),
                    Title = "Title of Dissertaion",
                    Description = "Description of Dissertaion",
                    Aim = "Aim of Dissertaion",
                    ImageUrl = "Image/Image.jpg"
                });

            context.SaveChanges();
            #endregion
        }
    }
}
