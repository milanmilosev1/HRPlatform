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

        public async Task<Candidate?> GetCandidateByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<List<Candidate>> GetCandidatesAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task RemoveCandidateAsync(Guid id)
        {
            var candidate = await _repo.GetByIdAsync(id) 
                ?? throw new KeyNotFoundException($"Candidate with the id: {id} does not exist");

            await _repo.RemoveAsync(candidate);
        }

        public async Task<Candidate> UpdateCandidateInfoAsync(UpdateCandidateDTO newCandidate)
        {
            var candidate = await _repo.GetByIdAsync(newCandidate.Id)
                ?? throw new KeyNotFoundException($"Candidate with the id: {newCandidate.Id} does not exist");

            candidate.Name = newCandidate.Name;
            candidate.Email = newCandidate.Email;
            candidate.ContactNumber = newCandidate.ContactNumber;
            candidate.DateOfBirth = newCandidate.DateOfBirth;

            candidate.CandidateSkills.Clear();

            foreach (var skillId in newCandidate.SkillIds)
            {
                candidate.CandidateSkills.Add(new CandidateSkills
                {
                    CandidateId = candidate.Id,
                    SkillId = skillId
                });
            }

            await _repo.SaveChangesAsync();

            return candidate;
        }
    }
}
