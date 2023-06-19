using PersonalProfileAPI.Models.Domains;
using PersonalProfileAPI.Models.DTOs;

namespace PersonalProfileAPI.Repository
{
    public interface IEducationRepository
    {
        public Task<List<Education>> GetAll();

        public Task<Education?> GetById(Guid id);
    }
}
