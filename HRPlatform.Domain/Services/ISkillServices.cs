using HRPlatform.Domain.DTOs.SkillDTOs;
using HRPlatform.Domain.Models;

namespace HRPlatform.Domain.Services
{
    public interface ISkillServices
    {
        Task<Skill> AddSkillAsync(CreateSkillDTO skill);
        Task RemoveSkillAsync(Guid id);
        Task<Skill?> UpdateSkillInfoAsync(UpdateSkillDTO newSkill);
        Task<Skill?> GetSkillByIdAsync(Guid skillId);
        Task<List<Skill>> GetSkillsAsync();
    }
}
