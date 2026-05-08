using HRPlatform.Domain.DTOs.SkillDTOs;
using HRPlatform.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRPlatform.WebApi.Controllers
{
    [ApiController]
    [Route("/skills")]
    public class SkillController(ISkillServices service) : ControllerBase
    {
        private readonly ISkillServices _service = service;

        [HttpPost]
        [Route("/add-skill")]
        public async Task<IActionResult> AddSkillAsync(CreateSkillDTO skill)
        {
            var item = await _service.AddSkillAsync(skill);
            if (item is null)
                return BadRequest("Failed to add skill");

            return Ok(item);
        }
    }
}
