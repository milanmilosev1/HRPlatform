using HRPlatform.Domain.DTOs.CandidateDTOs;
using HRPlatform.Domain.Models;
using HRPlatform.Domain.Repositories;
using HRPlatform.Domain.Services;

namespace HRPlatform.Services.CandidateServices
{
    public class CandidateService(ICandidateRepository repo) : ICandidateServices
    {
        private readonly ICandidateRepository _repo = repo;
        public async Task<Candidate> AddCandidateAsync(CreateCandidateDTO candidate)
        {
            var newCandidate = new Candidate
            {
                Id = Guid.NewGuid(),
                Name = candidate.Name,
                ContactNumber = candidate.ContactNumber,
                Email = candidate.Email,
                DateOfBirth = candidate.DateOfBirth,
                CandidateSkills = []
            };

            foreach(var skillId in candidate.SkillIds)
            {
                newCandidate.CandidateSkills.Add(new CandidateSkills
                {
                    CandidateId = newCandidate.Id,
                    SkillId = skillId
                });
            }

            await _repo.AddAsync(newCandidate);
            
            return newCandidate;
        }

        public Task<Candidate> GetCandidateByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Candidate>> GetCandidatesAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveCandidateAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Candidate> UpdateCandidateInfoAsync(UpdateCandidateDTO newCandidate)
        {
            throw new NotImplementedException();
        }
    }
}
