using HRPlatform.Domain.Models;

namespace HRPlatform.Domain.Repositories
{
    public interface ISkillRepository
    {
        Task<Skill> AddAsync(Skill skill);
        Task RemoveAsync(Guid skillId);
        Task<Skill> UpdateAsync(Skill newSkill);
        Task<Skill> GetByIdAsync(Guid id);
        Task<List<Skill>> GetAllAsync();
    }
}
