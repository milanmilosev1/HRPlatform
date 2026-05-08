using HRPlatform.Domain.Models;
using HRPlatform.Domain.Repositories;

namespace HRPlatform.Data.Repositories.SkillRepository
{
    public class SkillRepository : ISkillRepository
    {
        public Task<Skill> AddAsync(Skill skill)
        {
            throw new NotImplementedException();
        }

        public Task<List<Skill>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Skill?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Skill skill)
        {
            throw new NotImplementedException();
        }

        public Task<Skill> UpdateAsync(Skill newSkill)
        {
            throw new NotImplementedException();
        }
    }
}
