using HRPlatform.Domain.DTOs.CandidateDTOs;
using HRPlatform.Domain.DTOs.SkillDTOs;
using HRPlatform.Domain.Models;

namespace HRPlatform.Services.Mappers
{
    public static class CandidateMapper
    {
        public static CandidateResponseDTO ToResponse(Candidate candidate)
        {
            return new CandidateResponseDTO
            {
                Id = candidate.Id,
                Name = candidate.Name,
                Email = candidate.Email,
                ContactNumber = candidate.ContactNumber,
                DateOfBirth = candidate.DateOfBirth,
                Skills = [.. candidate.CandidateSkills
                    .Select(cs => new SkillResponseDTO
                    {
                        Id = cs.Skill.Id,
                        Name = cs.Skill.Name
                    })]
            };
        }

        public static Candidate CreateDTOToModel(CreateCandidateDTO candidate)
        {
            var newCandidate = new Candidate
            {
                Id = Guid.NewGuid(),
                Name = candidate.Name,
                ContactNumber = candidate.ContactNumber,
                Email = candidate.Email,
                DateOfBirth = candidate.DateOfBirth,
                CandidateSkills = [.. candidate.SkillIds.Select(sid => new CandidateSkills
                {
                    CandidateId = Guid.Empty,
                    SkillId = sid
                })]
            };

            return newCandidate;
        }

        public static void UpdateDTOToModel(UpdateCandidateDTO dto, Candidate candidate)
        {
            candidate.Name = dto.Name;
            candidate.Email = dto.Email;
            candidate.ContactNumber = dto.ContactNumber;
            candidate.DateOfBirth = dto.DateOfBirth;

            var toRemove = candidate.CandidateSkills.Where(cs => !dto.SkillIds.Contains(cs.SkillId)).ToList();

            foreach (var item in toRemove)
            {
                candidate.CandidateSkills.Remove(item);
            }

            var existingIds = candidate.CandidateSkills.Select(cs => cs.SkillId).ToList();

            foreach (var skillId in dto.SkillIds.Where(id => !existingIds.Contains(id)))
            {
                candidate.CandidateSkills.Add(new CandidateSkills
                {
                    CandidateId = candidate.Id,
                    SkillId = skillId
                });
            }
        }
    }
}
