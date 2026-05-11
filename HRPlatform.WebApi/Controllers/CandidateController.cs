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
            var result = await _service.AddCandidateAsync(candidate);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPatch]
        [Route("/update-candidate")]
        public async Task<IActionResult> UpdateCandidateInfo([FromBody] UpdateCandidateDTO candidate)
        {
            var result = await _service.UpdateCandidateInfoAsync(candidate);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("/all-candidates")]
        public async Task<IActionResult> GetAllCandidatesAsync()
        {
            var result = await _service.GetCandidatesAsync();
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet]
        [Route($"/candidate-id:")]
        public async Task<IActionResult> GetCandidateByIdAsync([FromQuery] Guid id)
        {
            var result = await _service.GetCandidateByIdAsync(id);
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("/candidates-by-skills")]
        public async Task<IActionResult> GetCandidatesBySkills([FromQuery] List<Guid> skillIds)
        {
            var result = await _service.GetCandidatesBySkills(skillIds);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("/candidates-by-name")]
        public async Task<IActionResult> GetCandidatesByName([FromQuery] string name)
        {
            var result = await _service.GetCandidatesByName(name);
            if(result.IsFailure || result.Value.Count == 0)
            {
                return NotFound("No candidates with this name found");
            }

            return Ok(result.Value);
        }

        [HttpDelete]
        [Route("/remove-candidate")]
        public async Task<IActionResult> DeleteCandidateAsync([FromBody] Guid candidateId)
        {
            var result = await _service.RemoveCandidateAsync(candidateId);
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok("Candidate removede succesfully");
        }

        [HttpPatch]
        [Route("/remove-candidate-skill")]
        public async Task<IActionResult> RemoveSkillFromCandidateAsync([FromQuery] Guid candidateId, [FromQuery] string skillName)
        {
            var result = await _service.RemoveSkillFromCandidateAsync(candidateId, skillName);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
