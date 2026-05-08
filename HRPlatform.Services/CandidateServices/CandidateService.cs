using HRPlatform.Domain.Models;
using HRPlatform.Domain.Services;

namespace HRPlatform.Services.CandidateServices
{
    public class CandidateService : ICandidateServices
    {
        public Task<Candidate> AddCandidateAsync(Candidate candidate)
        {
            throw new NotImplementedException();
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

        public Task<Candidate> UpdateCandidateInfoAsync(Candidate newCandidate)
        {
            throw new NotImplementedException();
        }
    }
}
