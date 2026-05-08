using HRPlatform.Domain.Models;
using HRPlatform.Domain.Repositories;

namespace HRPlatform.Data.Repositories.CandidateRepository
{
    public class CandidateRepository : ICandidateRepository
    {
        public Task<Candidate> AddAsync(Candidate candidate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Candidate>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Candidate> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid candidateId)
        {
            throw new NotImplementedException();
        }

        public Task<Candidate> UpdateAsync(Candidate newCandidate)
        {
            throw new NotImplementedException();
        }
    }
}
