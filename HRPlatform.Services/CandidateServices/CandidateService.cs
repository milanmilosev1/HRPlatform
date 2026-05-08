using HRPlatform.Domain.DTOs.CandidateDTOs;
using HRPlatform.Domain.Repositories;
using HRPlatform.Domain.Services;
using HRPlatform.Services.Mappers;

namespace HRPlatform.Services.CandidateServices
{
    public class CandidateService(ICandidateRepository repo) : ICandidateServices
    {
        private readonly ICandidateRepository _repo = repo;
        public async Task<CandidateResponseDTO?> AddCandidateAsync(CreateCandidateDTO candidate)
        {
            if (candidate is null)
                return null;

            //TODO: Validate

            var newCandidate = CandidateMapper.CreateDTOToModel(candidate);

            var saved = await _repo.AddAsync(newCandidate);

            if (saved is null)
                return null;

            return CandidateMapper.ToResponse(newCandidate);
        }

        public async Task<CandidateResponseDTO?> GetCandidateByIdAsync(Guid id)
        {
            var candidate = await _repo.GetByIdAsync(id);

            if (candidate is null)
                return null;

            return CandidateMapper.ToResponse(candidate);
        }

        public async Task<List<CandidateResponseDTO>> GetCandidatesAsync()
        {
            var candidates = await _repo.GetAllAsync();
            return [.. candidates.Select(CandidateMapper.ToResponse)];
        }

        public async Task<List<CandidateResponseDTO>> GetCandidatesByName(string name)
        {
            var all = await _repo.GetAllAsync();
            var filtered = all.Where(x => x.Name.Contains(name)).ToList();

            return [.. filtered.Select(CandidateMapper.ToResponse)];
        }

        public async Task<List<CandidateResponseDTO>> GetCandidatesBySkills(List<Guid> skillIds)
        {
            var all = await _repo.GetAllAsync();
            var filtered = all.Where(x => x.CandidateSkills.Any(cs => skillIds.Contains(cs.SkillId))).ToList();

            return [.. filtered.Select(CandidateMapper.ToResponse)];
        }

        public async Task RemoveCandidateAsync(Guid id)
        {
            var candidate = await _repo.GetByIdAsync(id) 
                ?? throw new KeyNotFoundException($"Candidate with the id: {id} does not exist");

            await _repo.RemoveAsync(candidate);
        }

        public async Task<CandidateResponseDTO?> UpdateCandidateInfoAsync(UpdateCandidateDTO newCandidate)
        {
            var candidate = await _repo.GetByIdAsync(newCandidate.Id)
                ?? throw new KeyNotFoundException($"Candidate with the id: {newCandidate.Id} does not exist");

            CandidateMapper.UpdateDTOToModel(newCandidate, candidate);

            await _repo.SaveChangesAsync();

            return CandidateMapper.ToResponse(candidate);
        }

        public async Task<CandidateResponseDTO?> RemoveSkillFromCandidateAsync(Guid candidateId, string skillName)
        {
            var candidate = await _repo.GetByIdAsync(candidateId);

            if (candidate is null)
                return null;

            var skillToRemove = candidate.CandidateSkills.FirstOrDefault(x => x.Skill.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase));
            if (skillToRemove != null)
            {
                candidate.CandidateSkills.Remove(skillToRemove);
                await _repo.SaveChangesAsync();
            }

            return CandidateMapper.ToResponse(candidate);
        }
    }
}
