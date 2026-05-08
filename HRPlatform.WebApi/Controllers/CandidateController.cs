using HRPlatform.Domain.DTOs.CandidateDTOs;
using HRPlatform.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRPlatform.WebApi.Controllers
{
    [ApiController]
    [Route("/candidate")]
    public class CandidateController(ICandidateServices service) : ControllerBase
    {
        private readonly ICandidateServices _service = service;

        [HttpPost]
        [Route("/add-candidate")]
        public async Task<IActionResult> AddCandidateAsync([FromBody] CreateCandidateDTO candidate)
        {
            var item = await _service.AddCandidateAsync(candidate);
            if (item is null)
            {
                return BadRequest("Falied to add candidate");
            }

            return Ok(item);
        }

        [HttpPatch]
        [Route("/update-candidate")]
        public async Task<IActionResult> UpdateCandidateInfo([FromBody] UpdateCandidateDTO candidate)
        {
            var item = await _service.UpdateCandidateInfoAsync(candidate);
            if (item is null)
                return BadRequest("Failed to update candidate");

            return Ok(item);
        }

        [HttpGet]
        [Route("/all-candidates")]
        public async Task<IActionResult> GetAllCandidatesAsync()
        {
            return Ok(await _service.GetCandidatesAsync());
        }

        [HttpGet]
        [Route($"/candidate-id:")]
        public async Task<IActionResult> GetCandidateByIdAsync([FromQuery] Guid id)
        {
            var item = await _service.GetCandidateByIdAsync(id);
            if (item is null)
                return NotFound("Candidate not found");

            return Ok(item);
        }

        [HttpGet]
        [Route("/candidates-by-skills")]
        public async Task<IActionResult> GetCandidatesBySkills([FromQuery] List<Guid> skillIds)
        {
            return Ok(await _service.GetCandidatesBySkills(skillIds));
        }

        [HttpGet]
        [Route("/candidates-by-name")]
        public async Task<IActionResult> GetCandidatesByName([FromQuery] string name)
        {
            var item = await _service.GetCandidatesByName(name);
            if(item.Count == 0)
            {
                return NotFound("No candidates with this name found");
            }

            return Ok(item);
        }

        [HttpDelete]
        [Route("/delete-candidate")]
        public async Task<IActionResult> DeleteCandidateAsync([FromBody] Guid candidateId)
        {
            await _service.RemoveCandidateAsync(candidateId);
            return Ok("Candidate removede succesfully");
        }

        [HttpPatch]
        [Route("/remove-candidate-skill")]
        public async Task<IActionResult> RemoveSkillFromCandidateAsync([FromQuery] Guid candidateId, [FromQuery] string skillName)
        {
            var item = await _service.RemoveSkillFromCandidateAsync(candidateId, skillName);
            if (item is null)
                return BadRequest();

            return Ok(item);
        }
    }
}
