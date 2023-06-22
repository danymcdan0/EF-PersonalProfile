using Microsoft.EntityFrameworkCore;
using PersonalProfileAPI.Data;
using PersonalProfileAPI.Models.Domains;

namespace PersonalProfileAPI.Repository
{
    public class EducationRepository : IEducationRepository
    {
        private readonly PersonalProfileDbContext dbContext;

        public EducationRepository(PersonalProfileDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Education> CreateAsync(Education education)
        {
            await dbContext.Education.AddAsync(education);
            await dbContext.SaveChangesAsync();
            return education;
        }

        public async Task<List<Education>> GetAllAsync()
        {
            return await dbContext.Education.ToListAsync();
        }

        public async Task<Education?> GetByIdAsync(Guid id)
        {
            var education = await dbContext.Education.FirstOrDefaultAsync(e => e.Id == id);
            return education;
        }

        public async Task<Education?> DeleteAsync(Guid id)
        {
            var education = await dbContext.Education.FirstOrDefaultAsync(e => e.Id == id);
            if (education !=  null)
            {
                dbContext.Remove(education);
                await dbContext.SaveChangesAsync();
            }
            return education;
        }

        public async Task<Education?> UpdateAsync(Guid id, Education education)
        {
            var existingEducation = await dbContext.Education.FirstOrDefaultAsync((e) => e.Id == id);
            if (existingEducation != null) {
                existingEducation.University = education.University;
                existingEducation.Course = education.Course;
                existingEducation.Grade = education.Grade;
                existingEducation.Description = education.Description;
                existingEducation.StartDate = education.StartDate;
                existingEducation.EndDate = education.EndDate;

                await dbContext.SaveChangesAsync();
            }
            return existingEducation;
        }
    }
}
