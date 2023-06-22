using Microsoft.EntityFrameworkCore;
using PersonalProfileAPI.Data;
using PersonalProfileAPI.Models.Domains;

namespace PersonalProfileAPI.Repository
{
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly PersonalProfileDbContext dbContext;

        public ExperienceRepository(PersonalProfileDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        public async Task<Experience> CreateAsync(Experience experience)
        {
            await dbContext.AddAsync(experience);
            await dbContext.SaveChangesAsync();
            return experience;
        }

        public async Task<Experience?> DeleteAsync(Guid id)
        {
            var experience = await dbContext.Experiences.FirstOrDefaultAsync(e => e.Id == id);
            if (experience != null) 
            {
                dbContext.Experiences.Remove(experience); 
                await dbContext.SaveChangesAsync();
            }
            return experience;
        }

        public async Task<List<Experience>> GetAllAsync()
        {
            return await dbContext.Experiences.ToListAsync();
        }

        public async Task<Experience?> GetByIdAsync(Guid id)
        {
            var experience = await dbContext.Experiences.FirstOrDefaultAsync(e => e.Id == id);
            return experience;
        }

        public async Task<Experience?> UpdateAsync(Guid id, Experience experience)
        {
            var existingExperience = await dbContext.Experiences.FirstOrDefaultAsync(e => e.Id == id);
            if (existingExperience != null) 
            {
                existingExperience.Company = experience.Company;
                existingExperience.Role = experience.Role;
                existingExperience.Description = experience.Description;
                existingExperience.StartDate = experience.StartDate;
                existingExperience.EndDate = experience.EndDate;

                await dbContext.SaveChangesAsync();
            }
            return existingExperience;
        }
    }
}
