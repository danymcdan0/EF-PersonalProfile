using Microsoft.EntityFrameworkCore;
using PersonalProfileAPI.Data;
using PersonalProfileAPI.Models.Domains;
using PersonalProfileAPI.Models.DTOs;

namespace PersonalProfileAPI.Repository
{
    public class EducationRepository : IEducationRepository
    {
        private readonly PersonalProfileDbContext dbContext;

        public EducationRepository(PersonalProfileDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Education>> GetAll()
        {
            return await dbContext.Education.ToListAsync();
        }

        public async Task<Education?> GetById(Guid id)
        {
            var education = await dbContext.Education.FirstOrDefaultAsync(e => e.Id == id);
            if (education == null)
            {
                return null;
            }
            return education;
        }
    }
}
