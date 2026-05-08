using HRPlatform.Domain.DTOs.SkillDTOs;
using HRPlatform.Domain.Models;
using HRPlatform.Domain.Repositories;
using HRPlatform.Domain.Services;

namespace HRPlatform.Services.SkillServices
{
    public class SkillService(ISkillRepository repo) : ISkillServices
    {
        private readonly ISkillRepository _repo = repo;
        public async Task<Skill> AddSkillAsync(CreateSkillDTO skill)
        {
            var newSkill = new Skill
            {
                Id = Guid.NewGuid(),
                Name = skill.Name,
                CandidateSkills = []
            };

            await _repo.AddAsync(newSkill);
            await _repo.SaveChangesAsync();

            return newSkill;
        }

        public async Task<Skill?> GetSkillByIdAsync(Guid skillId)
        {
            return await _repo.GetByIdAsync(skillId);
        }

        public async Task<List<Skill>> GetSkillsAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task RemoveSkillAsync(Guid id)
        {
            var skill = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Skill with id: {id} does not exist");

            await _repo.RemoveAsync(skill);
        }

        public async Task<Skill?> UpdateSkillInfoAsync(UpdateSkillDTO newSkill)
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

            return skill;
        }
    }
}
