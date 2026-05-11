using HRPlatform.Domain.Common;
using HRPlatform.Domain.DTOs.CandidateDTOs;
using HRPlatform.Domain.Repositories;
using HRPlatform.Domain.Services;
using HRPlatform.Services.Mappers;
using HRPlatform.Services.Validators;

namespace HRPlatform.Services.CandidateServices
{
    public class CandidateService(ICandidateRepository repo) : ICandidateServices
    {
        private readonly ICandidateRepository _repo = repo;
        public async Task<Result<CandidateResponseDTO>> AddCandidateAsync(CreateCandidateDTO candidate)
        {
            if (candidate is null)
                return Result<CandidateResponseDTO>.Failure("Candidate data cannot be null.");

            var newCandidate = CandidateMapper.CreateDTOToModel(candidate);

            var validationResult = CandidateValidator.Validate(newCandidate);

            if (!validationResult.Message.Equals(string.Empty))
                return Result<CandidateResponseDTO>.Failure(validationResult.Message);

            var saved = await _repo.AddAsync(newCandidate);

            if (saved is null)
                return Result<CandidateResponseDTO>.Failure("Failed to save candidate to the database.");

            return Result<CandidateResponseDTO>.Success(CandidateMapper.ToResponse(newCandidate));
        }

        public async Task<Result<CandidateResponseDTO>> GetCandidateByIdAsync(Guid id)
        {
            var candidate = await _repo.GetByIdAsync(id);

            if (candidate is null)
                return Result<CandidateResponseDTO>.Failure($"Candidate with the id: {id} does not exist.");

            return Result<CandidateResponseDTO>.Success(CandidateMapper.ToResponse(candidate));
        }

        public async Task<Result<List<CandidateResponseDTO>>> GetCandidatesAsync()
        {
            var candidates = await _repo.GetAllAsync();
            return Result<List<CandidateResponseDTO>>.Success([.. candidates.Select(CandidateMapper.ToResponse)]);
        }

        public async Task<Result<List<CandidateResponseDTO>>> GetCandidatesByName(string name)
        {
            var all = await _repo.GetAllAsync();
            var filtered = all.Where(x => x.Name.Contains(name)).ToList();

            return Result<List<CandidateResponseDTO>>.Success([.. filtered.Select(CandidateMapper.ToResponse)]);
        }

        public async Task<Result<List<CandidateResponseDTO>>> GetCandidatesBySkills(List<Guid> skillIds)
        {
            var all = await _repo.GetAllAsync();
            var filtered = all.Where(x => x.CandidateSkills.Any(cs => skillIds.Contains(cs.SkillId))).ToList();

            return Result<List<CandidateResponseDTO>>.Success([.. filtered.Select(CandidateMapper.ToResponse)]);
        }

        public async Task<Result> RemoveCandidateAsync(Guid id)
        {
            var candidate = await _repo.GetByIdAsync(id);
            if (candidate is null)
                return Result.Failure($"Candidate with the id: {id} does not exist.");

            await _repo.RemoveAsync(candidate);
            return Result.Success();
        }

        public async Task<Result<CandidateResponseDTO>> UpdateCandidateInfoAsync(UpdateCandidateDTO newCandidate)
        {
            var candidate = await _repo.GetByIdAsync(newCandidate.Id);
            if (candidate is null)
                return Result<CandidateResponseDTO>.Failure($"Candidate with the id: {newCandidate.Id} does not exist.");

            CandidateMapper.UpdateDTOToModel(newCandidate, candidate);

            await _repo.SaveChangesAsync();

            return Result<CandidateResponseDTO>.Success(CandidateMapper.ToResponse(candidate));
        }

        public async Task<Result<CandidateResponseDTO>> RemoveSkillFromCandidateAsync(Guid candidateId, string skillName)
        {
            if (string.IsNullOrWhiteSpace(skillName))
                return Result<CandidateResponseDTO>.Failure("Skill name cannot be empty.");

            var candidate = await _repo.GetByIdAsync(candidateId);

            if (candidate is null)
                return Result<CandidateResponseDTO>.Failure($"Candidate with the id: {candidateId} does not exist.");

            var skillToRemove = candidate.CandidateSkills.FirstOrDefault(x => x.Skill.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase));
            if (skillToRemove != null)
            {
                candidate.CandidateSkills.Remove(skillToRemove);
                await _repo.SaveChangesAsync();
            }
            else
            {
                return Result<CandidateResponseDTO>.Failure($"Candidate does not possess the skill '{skillName}'.");
            }

            return Result<CandidateResponseDTO>.Success(CandidateMapper.ToResponse(candidate));
        }
    }
}
