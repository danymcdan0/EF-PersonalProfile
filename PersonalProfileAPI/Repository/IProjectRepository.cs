using PersonalProfileAPI.Models.Domains;

namespace PersonalProfileAPI.Repository
{
    public interface IProjectRepository
    {
        public Task<List<Project>> GetAllAsync();

        public Task<Project?> GetByIdAsync(Guid id);

        public Task<Project> CreateAsync(Project project);

        public Task<Project?> DeleteAsync(Guid id);

        public Task<Project?> UpdateAsync(Guid id, Project project);
    }
}
