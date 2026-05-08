using HRPlatform.Domain.DTOs.SkillDTOs;
using HRPlatform.Domain.Models;
using HRPlatform.Domain.Repositories;
using HRPlatform.Domain.Services;
using HRPlatform.Services.Mappers;

namespace HRPlatform.Services.SkillServices
{
    public class SkillService(ISkillRepository repo) : ISkillServices
    {
        private readonly ISkillRepository _repo = repo;
        public async Task<SkillResponseDTO> AddSkillAsync(CreateSkillDTO skill)
        {
            var newSkill = new Skill
            {
                Id = Guid.NewGuid(),
                Name = skill.Name,
                CandidateSkills = []
            };

            await _repo.AddAsync(newSkill);
            await _repo.SaveChangesAsync();

            return SkillMapper.ToResponse(newSkill);
        }

        public async Task<SkillResponseDTO?> GetSkillByIdAsync(Guid skillId)
        {
            var skill = await _repo.GetByIdAsync(skillId);
            if (skill is null)
                return null;

            return SkillMapper.ToResponse(skill);
        }

        public async Task<List<SkillResponseDTO>> GetSkillsAsync()
        {
            var skills = await _repo.GetAllAsync();
            return [.. skills.Select(SkillMapper.ToResponse)];
        }

        public async Task RemoveSkillAsync(Guid id)
        {
            var skill = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Skill with id: {id} does not exist");

            await _repo.RemoveAsync(skill);
        }

        public async Task<SkillResponseDTO?> UpdateSkillInfoAsync(UpdateSkillDTO newSkill)
        {
            var skill = await _repo.GetByIdAsync(newSkill.Id);

            if (skill is null)
                return null;

            skill.Name = newSkill.Name;

            skill.CandidateSkills.Clear();

            foreach (var candidateId in newSkill.CandidateIds)
            {
                skill.CandidateSkills.Add(new CandidateSkills
                {
                    SkillId = skill.Id,
                    CandidateId = candidateId
                });
            }

            await _repo.SaveChangesAsync();

            return SkillMapper.ToResponse(skill);
        }
    }
}
