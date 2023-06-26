using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalProfileAPI.CustomActionFilters;
using PersonalProfileAPI.Models.Domains;
using PersonalProfileAPI.Models.DTOs;
using PersonalProfileAPI.Repository;
using System.Data;

namespace PersonalProfileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository projectRepository;
        private readonly IMapper mapper;

        public ProjectController(IProjectRepository projectRepository, IMapper mapper)
        {
            this.projectRepository = projectRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var projectDomains = await projectRepository.GetAllAsync();
            var projectDTOs = mapper.Map<List<ProjectDTO>>(projectDomains);
            return Ok(projectDTOs);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var projectDomain = await projectRepository.GetByIdAsync(id);
            if (projectDomain == null)
            {
                return NotFound(id);
            }
            var projectDTO = mapper.Map<ProjectDTO>(projectDomain);
            return Ok(projectDTO);
        }

		[Authorize(Roles = "Owner")]
		[HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] AddProjectDTO addProjectDTO)
        {
            var projectDomain = mapper.Map<Project>(addProjectDTO);
            var createProject = await projectRepository.CreateAsync(projectDomain);
            if (createProject != null)
            {
                return Ok(mapper.Map<ProjectDTO>(projectDomain));
            }
            return BadRequest();
        }

		[Authorize(Roles = "Owner")]
		[HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateProjectDTO updateProjectDTO)
        {
            var projectDomain = mapper.Map<Project>(updateProjectDTO);
            var updateProject = await projectRepository.UpdateAsync(id, projectDomain);
            if (updateProject == null)
            {
                return NotFound(id);
            }
            return Ok(mapper.Map<ProjectDTO>(projectDomain));
        }

		[Authorize(Roles = "Owner")]
		[HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var projectDomain = await projectRepository.DeleteAsync(id);
            if (projectDomain == null) { return NotFound(id); }
            return Ok(mapper.Map<ProjectDTO>(projectDomain));
        }
    }
}
