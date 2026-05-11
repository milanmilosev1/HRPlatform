using HRPlatform.Domain.Common;
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
        public async Task<Result<SkillResponseDTO>> AddSkillAsync(CreateSkillDTO skill)
        {
            if (skill is null)
                return Result<SkillResponseDTO>.Failure("Skill data cannot be null");

            var existingSkills = await _repo.GetAllAsync();
            if (existingSkills.Any(s => s.Name.Equals(skill.Name, StringComparison.OrdinalIgnoreCase)))
                return Result<SkillResponseDTO>.Failure($"Skill with name '{skill.Name}' already exists.");

            var newSkill = new Skill
            {
                Id = Guid.NewGuid(),
                Name = skill.Name,
                CandidateSkills = []
            };

            await _repo.AddAsync(newSkill);
            await _repo.SaveChangesAsync();

            return Result<SkillResponseDTO>.Success(SkillMapper.ToResponse(newSkill));
        }

        public async Task<Result<SkillResponseDTO>> GetSkillByIdAsync(Guid skillId)
        {
            var skill = await _repo.GetByIdAsync(skillId);
            if (skill is null)
                return Result<SkillResponseDTO>.Failure($"Skill with id: {skillId} does not exist");

            return Result<SkillResponseDTO>.Success(SkillMapper.ToResponse(skill));
        }

        public async Task<Result<List<SkillResponseDTO>>> GetSkillsAsync()
        {
            var skills = await _repo.GetAllAsync();
            return Result<List<SkillResponseDTO>>.Success([.. skills.Select(SkillMapper.ToResponse)]);
        }

        public async Task<Result> RemoveSkillAsync(Guid id)
        {
            var skill = await _repo.GetByIdAsync(id);
            if (skill is null)
                return Result.Failure($"Skill with id: {id} does not exist");

            await _repo.RemoveAsync(skill);
            return Result.Success();
        }

        public async Task<Result<SkillResponseDTO>> UpdateSkillInfoAsync(UpdateSkillDTO newSkill)
        {
            var skill = await _repo.GetByIdAsync(newSkill.Id);

            if (skill is null)
                return Result<SkillResponseDTO>.Failure($"Skill with id: {newSkill.Id} does not exist");

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

            return Result<SkillResponseDTO>.Success(SkillMapper.ToResponse(skill));
        }
    }
}
