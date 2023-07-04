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
                    Role = "C# Consultant",
                    Description = "8 Week Training: Agile & Scrum, Version Control Github, SQL, C#, .NET 7, OOP, SOLD Principles, Design Patterns, Unit Testing (NUnit) & " +
                    "Mocking (Moq), Entity Framework, LINQ, Asynchronous Programming, Rest APIs, HTML/CSS/JavaScript, ASP.NET Core MVC for API and Web, ASP.NET Core Razor Pages. " +
                    "After training I have been in Pre-assignment awaiting placement with a client, and during this time period I have been upskilling with ASP.NET learning " +
                    "to utilise Cookies and JWT Tokens for Authentication, and Azure for making APIs/Websites public.",
                    StartDate = new DateTime(2023, 3, 20)
                });

            context.Education.AddRange(
                new Education {
                    Id = Guid.NewGuid(),
                    University = "University of Essex",
                    Course = "BSC Computer Science",
                    Grade = "1st",
                    Description = "Studied courses: Team Project Challenge, Mathematics for Computing, Object-Oriented Programming, Intro to Databases, " +
                    "Web Development, Human- Computer Interaction, Network Fundamentals, Fundamentals of Digital Systems, Software Engineering, Application Programming, " +
                    "Data Structures and Algorithms, Artificial Intelligence, Computer Game Programming, Computer Security, Advanced Programming, Information Retrieval, " +
                    "Natural Language Engineering, Computer Vision, Network Security.",
                    StartDate = new DateTime(2018, 10, 1),
                    EndDate = new DateTime(2021, 7, 20)
                },
                new Education
                {
                    Id = Guid.NewGuid(),
                    University = "University of Essex",
                    Course = "MSC Game Development",
                    Grade = "Merit",
                    Description = "Studied courses: Game Design, Machine Learning, Group Project (Incorporating a Game Jam), " +
                    "MSc Project & Dissertation, Game Artificial Intelligence, Physics Based Games, Mobile and Social Application Programming, " +
                    "Professional Practice and Research Methodology.",
                    StartDate = new DateTime(2021, 10, 1),
                    EndDate = new DateTime(2022, 12, 1)
                });

            context.Projects.AddRange(
                new Project {
                    Id = Guid.NewGuid(),
                    Title = "DropSort - Udergrad Dissertation",
                    Description = "Description of Project",
                    Aim = "Aim of Project"
                },
                new Project
                {
                    Id = Guid.NewGuid(),
                    Title = "5 Year Mission - Postgrad Dissertation",
                    Description = "Description of Dissertaion",
                    Aim = "Aim of Dissertaion",
                    ImageUrl = "Image/Image.jpg"
                },
                new Project
                {
                    Id = Guid.NewGuid(),
                    Title = "Trainee Tracker API",
                    Description = "",
                    Aim = "",
                    ImageUrl = "Image/Image.jpg"
                },
                new Project
                {
                    Id = Guid.NewGuid(),
                    Title = "Personal Profile Website",
                    Description = "",
                    Aim = "",
                    ImageUrl = "Image/Image.jpg"
                });

            context.SaveChanges();
            #endregion
        }
    }
}
