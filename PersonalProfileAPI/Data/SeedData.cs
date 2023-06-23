using PersonalProfileAPI.Models.Domains;

namespace PersonalProfileAPI.Data
{
    public class SeedData
    {
        public static void Initialise(IServiceProvider serviceProvider) 
        {
            var context = serviceProvider.GetRequiredService<PersonalProfileDbContext>();

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
        }
    }
}
