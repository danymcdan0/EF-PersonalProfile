using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProfileAPI.Models.Domains;
using PersonalProfileAPI.Models.DTOs;
using PersonalProfileAPI.Repository;
using PersonalProfileAPI.CustomActionFilters;

namespace PersonalProfileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IEducationRepository educationRepository;
        private readonly IMapper mapper;

        public EducationController(IEducationRepository educationRepository, IMapper mapper)
        {
            this.educationRepository = educationRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var educationDomains = await educationRepository.GetAllAsync();
            var educactionDTOs = mapper.Map<List<EducationDTO>>(educationDomains);
            return Ok(educactionDTOs);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var educationDomain = await educationRepository.GetByIdAsync(id);
            if (educationDomain == null)
            {
                return NotFound();
            }
            var educactionDTO = mapper.Map<EducationDTO>(educationDomain);
            return Ok(educactionDTO);
        }


        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddEducationDTO addEducationDTO) 
        {
            var educationDomain = mapper.Map<Education>(addEducationDTO);
            var createEducation = await educationRepository.CreateAsync(educationDomain);
            return Ok(mapper.Map<EducationDTO>(educationDomain));
        }

        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateEducationDTO updateEducationDTO) 
        {
            var educationDomain = mapper.Map<Education>(updateEducationDTO);
            var updateEducation = await educationRepository.UpdateAsync(id, educationDomain);
            if (updateEducation == null)
            {
                return NotFound(id);
            }
            return Ok(mapper.Map<EducationDTO>(educationDomain));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id) 
        {
            var educationDomain = await educationRepository.DeleteAsync(id);
            if (educationDomain == null)
            {
                return NotFound(id);
            }
            return Ok(mapper.Map<EducationDTO>(educationDomain));
        }
    }
}
