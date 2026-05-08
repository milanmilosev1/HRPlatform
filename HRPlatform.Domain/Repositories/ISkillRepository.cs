using HRPlatform.Domain.Models;

namespace HRPlatform.Domain.Repositories
{
    public interface ISkillRepository
    {
        Task<Skill> AddAsync(Skill skill);
        Task RemoveAsync(Skill skill);
        Task<Skill?> GetByIdAsync(Guid id);
        Task<List<Skill>> GetAllAsync();
        Task SaveChangesAsync();

    }
}
