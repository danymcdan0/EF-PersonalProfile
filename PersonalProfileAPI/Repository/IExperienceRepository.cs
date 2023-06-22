using PersonalProfileAPI.Models.Domains;

namespace PersonalProfileAPI.Repository
{
    public interface IExperienceRepository
    {
        public Task<List<Experience>> GetAllAsync();

        public Task<Experience?> GetByIdAsync(Guid id);

        public Task<Experience> CreateAsync(Experience experience);

        public Task<Experience?> DeleteAsync(Guid id);

        public Task<Experience?> UpdateAsync(Guid id, Experience experience);
    }
}
