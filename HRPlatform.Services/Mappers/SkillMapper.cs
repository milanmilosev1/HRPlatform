using HRPlatform.Domain.DTOs.SkillDTOs;
using HRPlatform.Domain.Models;

namespace HRPlatform.Services.Mappers
{
    public static class SkillMapper
    {
        public static SkillResponseDTO ToResponse(Skill skill)
        {
            return new SkillResponseDTO
            {
                Id = skill.Id,
                Name = skill.Name
            };
        }

        public static Skill CreateDTOToModel(CreateSkillDTO skill)
        {
            return new Skill
            {
                Id = Guid.NewGuid(),
                Name = skill.Name,
                CandidateSkills = []
            };
        }
    }
}
