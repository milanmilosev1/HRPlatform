using HRPlatform.Domain.Models;
using HRPlatform.Domain.Services;

namespace HRPlatform.Services.SkillServices
{
    public class SkillService : ISkillServices
    {
        public Task<Skill> AddSkillAsync(Skill skill)
        {
            throw new NotImplementedException();
        }

        public Task<Skill> GetSkillByIdAsync(Guid skillId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Skill>> GetSkillsAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveSkillAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Skill> UpdateSkillInfoAsync(Skill newSkill)
        {
            throw new NotImplementedException();
        }
    }
}
