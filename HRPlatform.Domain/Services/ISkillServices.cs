using HRPlatform.Domain.Models;

namespace HRPlatform.Domain.Services
{
    public interface ISkillServices
    {
        Task<Skill> AddSkillAsync(Skill skill);
        Task RemoveSkillAsync(Guid id);
        Task<Skill> UpdateSkillInfoAsync(Skill newSkill);
        Task<Skill> GetSkillByIdAsync(Guid skillId);
        Task<List<Skill>> GetSkillsAsync();
    }
}
