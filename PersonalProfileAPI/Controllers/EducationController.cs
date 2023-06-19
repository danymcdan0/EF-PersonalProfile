using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProfileAPI.Models.DTOs;
using PersonalProfileAPI.Repository;

namespace PersonalProfileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly EducationRepository educationRepository;
        private readonly IMapper mapper;

        public EducationController(EducationRepository educationRepository, IMapper mapper)
        {
            this.educationRepository = educationRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var educationDomains = await educationRepository.GetAll();
            var educactionDTOs = mapper.Map<EducationDTO>(educationDomains);
            return Ok(educactionDTOs);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var educationDomain = await educationRepository.GetById(id);
            if (educationDomain == null)
            {
                return NotFound();
            }
            var educactionDTO = mapper.Map<EducationDTO>(educationDomain);
            return Ok(educactionDTO);
        }
    }
}
