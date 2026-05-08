using HRPlatform.Domain.DTOs.SkillDTOs;
using HRPlatform.Domain.Models;

namespace HRPlatform.Domain.Services
{
    public interface ISkillServices
    {
        Task<SkillResponseDTO> AddSkillAsync(CreateSkillDTO skill);
        Task RemoveSkillAsync(Guid id);
        Task<SkillResponseDTO?> UpdateSkillInfoAsync(UpdateSkillDTO newSkill);
        Task<SkillResponseDTO?> GetSkillByIdAsync(Guid skillId);
        Task<List<SkillResponseDTO>> GetSkillsAsync();
    }
}
