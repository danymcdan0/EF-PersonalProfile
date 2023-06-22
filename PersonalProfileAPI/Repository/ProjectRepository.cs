using Microsoft.EntityFrameworkCore;
using PersonalProfileAPI.Data;
using PersonalProfileAPI.Models.Domains;

namespace PersonalProfileAPI.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly PersonalProfileDbContext dbContext;

        public ProjectRepository(PersonalProfileDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Project> CreateAsync(Project project)
        {
            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();
            return project;
        }

        public async Task<Project?> DeleteAsync(Guid id)
        {
            var project = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project != null)
            {
                dbContext.Remove(project);
                await dbContext.SaveChangesAsync();
            }
            return project;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await dbContext.Projects.ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            var project = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
            return project;
        }

        public async Task<Project?> UpdateAsync(Guid id, Project project)
        {
            var existingProject = await dbContext.Projects.FirstOrDefaultAsync((e) => e.Id == id);
            if (existingProject != null)
            {
                existingProject.Title = project.Title;
                existingProject.Aim = project.Aim;
                existingProject.Description = project.Description;
                existingProject.ImageUrl = project.ImageUrl;

                await dbContext.SaveChangesAsync();
            }
            return existingProject;
        }
    }
}
