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
                    Role = "Junior C# Software Developer Consultant",
                    Description = "This opportunity began with 8 Week Training: Agile & Scrum, Version Control (Github/GIT), SQL, C#, .NET 7, OOP, SOLID Principles, Design Patterns, Unit Testing (NUnit) & " +
                    "Mocking (Moq), Entity Framework, LINQ, Asynchronous Programming, Rest APIs, HTML/CSS/JavaScript, ASP.NET Core MVC, ASP.NET API, Razor Pages. " +
                    "\nAfter training I have been in Pre-assignment awaiting placement with a client, and during this time period I have been upskilling with ASP.NET learning " +
                    "to utilise Cookies and JWT Tokens for Authentication, and Azure for making APIs/Websites public.",
                    StartDate = new DateTime(2023, 3, 20)
                });

            context.Education.AddRange(
                new Education {
                    Id = Guid.NewGuid(),
                    University = "University of Essex",
                    Course = "BSC Computer Science",
                    Grade = "1st",
                    Description = "Team Project Challenge, Mathematics for Computing, Object-Oriented Programming, Intro to Databases, " +
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
                    Description = "Game Design, Machine Learning, Group Project (Incorporating a Game Jam), " +
                    "MSc Project & Dissertation, Game Artificial Intelligence, Physics Based Games, Mobile and Social Application Programming, " +
                    "Professional Practice and Research Methodology.",
                    StartDate = new DateTime(2021, 10, 1),
                    EndDate = new DateTime(2022, 12, 1)
                });

            context.Projects.AddRange(
                new Project {
                    Id = Guid.NewGuid(),
                    Title = "Drop Sort - Undergrad Dissertation",
                    Description = "Drop Sort aims to tackle a core part of Pivotal Response Treatment known as conditional discrimination through training multiple cue " +
                    "responding which is where when asked the question: \"What color is the cat?\" The response of the child is \"meow\" instead of the color of the cat " +
                    "as the child only recognized 'cat'. Drop Sort is a sorting game created with C# in Unity utilising its navmesh agents, rigidbody, ontrigger methods " +
                    "and much more, where the player has to sort entities correctly according to cues, getting increasingly difficult over time. Further progress is saved " +
                    "over time to see the improvements of the player by a therapist which was one of the main aims of the project.",
                    Aim = "Drop Sort is a game targeted towards providing aid in the development of children with Autistic Spectrum Disorder. " +
                    "The objective of Drop Sort is to help reduce the strain on therapeutic services by exploring the fact that Children with autism are known to be \"heavy use gamers.\""
                },
                new Project
                {
                    Id = Guid.NewGuid(),
                    Title = "5 Year Mission - Postgrad Dissertation",
                    Description = "There are various different intentions behind a game, evoking different emotions which dictate gameplay, but this project will focus " +
                    "on games that attempt to form an attachment between the player and in-game entities. Colony simulation games are able to create a connection with " +
                    "the player while also providing the infrastructure for the player to make independent decisions, between prioritizing attachments or progression in " +
                    "the game. Research into an individual’s decision-making process in games has been scarcely documented compared to in the real world, hence this project" +
                    " aims to add to this field which is ripe for investigation. 5YM  was created with C# in Unity utilizing various algorithms such as Moore’s Neighbours " +
                    "and Perlin noise for the map generation, followed by a grid system for an A* search algorithm integrated with Unity’s Rigid body system for pathfinding." +
                    " The objective of the game is to aid a group of stranded colonists to reach their home planet by directing them to do tasks as an overlord, collecting " +
                    "the necessary resources for the final 5 Year Mission home. The game presents various scenarios in which the player is typically given two options, " +
                    "attachment or progression of the game where their decisions are recorded to be analysed for the research purpose of this project.",
                    Aim = "5 Year Mission is a colony simulation game targeted toward understanding the decision-making process of an individual while playing a game.",
                    ImageUrl = "Image/Image.jpg"
                },
                new Project
                {
                    Id = Guid.NewGuid(),
                    Title = "MVC Trainee Tracker API",
                    Description = "This web application serves as a centralised hub where trainees can conveniently log their weekly progress, ensuring that trainers have " +
                    "up-to-date and accurate information about each trainee's progress. It also offers trainees the flexibility to monitor their own development, " +
                    "review their previous entries, and make updates. To meet the specified requirements, the Trainee Tracker Web Application incorporates a " +
                    "controller and a service layer. These components are instrumental in enabling the functionality of the application, ensuring seamless communication " +
                    "between the user interface and the underlying data.",
                    Aim = "The primary objective of this project is to provide an efficient and user-friendly platform that allows trainees to update their progress " +
                    "on a weekly basis, enables trainers to view trainee trackers, and empowers trainees to create, edit, and delete their own trackers.",
                    ImageUrl = "Image/Image.jpg"
                },
                new Project
                {
                    Id = Guid.NewGuid(),
                    Title = "Personal Profile Website",
                    Description = "This project was to create an ASP.NET Core Web API that could be consumed by a MVC Web Application while following Restful practices. " +
                    "This web application contains the use of Controllers to direct URI navigation and Domain Models/DTOs for displaying and saving data in an SQL database " +
                    "through the use of asynchronous programming utilising the Repository Pattern. The website is publically available through the use of Azure. " +
                    "This website allows for the owner to be able to preform CRUD operations on a cloud database (Azure) if necessary through the site itself " +
                    "by utilising cookies and roles. Further, a standard viewer of the site is able to easily naviagate the site and find out information regarding me, my " +
                    "Education, Experiences and Projects. This website will be consistently updated in the furture with any new ventures I embark on making it a form of portfolio. " +
                    "New features or improvements will be worked on consistently regarding the information displayed on the site.  " +
                    "The result so far of this project is the site you are currently viewing!",
                    Aim = "To create a public facing online CV, utilising ASP.NET skills that I have learnt, containing all relevant information to do with me and my career in computer sceince.",
                    ImageUrl = "Image/Image.jpg"
                });

            context.SaveChanges();
            #endregion
        }
    }
}
