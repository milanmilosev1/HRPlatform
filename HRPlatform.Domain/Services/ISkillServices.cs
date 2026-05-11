using HRPlatform.Domain.Common;
using HRPlatform.Domain.DTOs.SkillDTOs;
using HRPlatform.Domain.Models;

namespace HRPlatform.Domain.Services
{
    public interface ISkillServices
    {
        Task<Result<SkillResponseDTO>> AddSkillAsync(CreateSkillDTO skill);
        Task<Result> RemoveSkillAsync(Guid id);
        Task<Result<SkillResponseDTO>> UpdateSkillInfoAsync(UpdateSkillDTO newSkill);
        Task<Result<SkillResponseDTO>> GetSkillByIdAsync(Guid skillId);
        Task<Result<List<SkillResponseDTO>>> GetSkillsAsync();
    }
}
