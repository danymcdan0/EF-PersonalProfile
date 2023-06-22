using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProfileAPI.CustomActionFilters;
using PersonalProfileAPI.Models.Domains;
using PersonalProfileAPI.Models.DTOs;
using PersonalProfileAPI.Repository;

namespace PersonalProfileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly IExperienceRepository experienceRepository;
        private readonly IMapper mapper;

        public ExperienceController(IExperienceRepository experienceRepository, IMapper mapper) 
        {
            this.experienceRepository = experienceRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() 
        {
            var experienceDomains = await experienceRepository.GetAllAsync();
            var experienceDTOs = mapper.Map<List<ExperienceDTO>>(experienceDomains);
            return Ok(experienceDTOs);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var ExperienceDomain = await experienceRepository.GetByIdAsync(id);
            if (ExperienceDomain == null)
            {
                return NotFound();
            }
            var educactionDTO = mapper.Map<ExperienceDTO>(ExperienceDomain);
            return Ok(educactionDTO);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] AddExperienceDTO addExperienceDTO)
        {
            var ExperienceDomain = mapper.Map<Experience>(addExperienceDTO);
            var createExperience = await experienceRepository.CreateAsync(ExperienceDomain);
            if (createExperience != null)
            {
                return Ok(mapper.Map<ExperienceDTO>(ExperienceDomain));
            }
            return BadRequest();
        }

        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateExperienceDTO updateExperienceDTO)
        {
            var experienceDomain = mapper.Map<Experience>(updateExperienceDTO);
            var updateExperience = await experienceRepository.UpdateAsync(id, experienceDomain);
            if (updateExperience == null)
            {
                return NotFound(id);
            }
            return Ok(mapper.Map<ExperienceDTO>(experienceDomain));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id) 
        {
            var experienceDomain = await experienceRepository.DeleteAsync(id);
            if (experienceDomain == null) { return NotFound(id); }
            return Ok(mapper.Map<ExperienceDTO>(experienceDomain));
        }
    }
}
