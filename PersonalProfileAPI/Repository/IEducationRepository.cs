using PersonalProfileAPI.Models.Domains;
using PersonalProfileAPI.Models.DTOs;

namespace PersonalProfileAPI.Repository
{
    public interface IEducationRepository
    {
        public Task<List<Education>> GetAllAsync();

        public Task<Education?> GetByIdAsync(Guid id);

        public Task<Education> CreateAsync(Education education);

        public Task<Education?> DeleteAsync(Guid id);

        public Task<Education?> UpdateAsync(Guid id, Education education);
    }
}
