using HRPlatform.Domain.DTOs.CandidateDTOs;
using HRPlatform.Domain.Models;
using HRPlatform.Domain.Repositories;
using HRPlatform.Services.CandidateServices;
using Moq;

namespace HRPlatform.Tests.CandidateServicesTests
{
    public class CandidateServiceTests
    {
        private readonly Mock<ICandidateRepository> _repoMock;
        private readonly CandidateService _candidateService;

        public CandidateServiceTests()
        {
            _repoMock = new Mock<ICandidateRepository>();
            _candidateService = new CandidateService(_repoMock.Object);
        }

        [Fact]
        public async Task AddCandidateAsync_WithNullCandidate_ReturnsFailureWithNullMessage()
        {
            // Act
            var result = await _candidateService.AddCandidateAsync(null!);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Candidate data cannot be null.", result.Error);
        }

        [Fact]
        public async Task AddCandidateAsync_WithEmptyName_ReturnsFailureMessage()
        {
            // Arrange
            var dto = new CreateCandidateDTO
            {
                Name = string.Empty,
                Email = "test@test.com",
                ContactNumber = "123456789"
            };

            // Act
            var result = await _candidateService.AddCandidateAsync(dto);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Candidate name not provided", result.Error);
        }

        [Fact]
        public async Task AddCandidateAsync_WithEmptyEmail_ReturnsFailureMessage()
        {
            // Arrange
            var dto = new CreateCandidateDTO
            {
                Name = "Milan Milan",
                Email = string.Empty,
                ContactNumber = "+381601231231"
            };

            // Act
            var result = await _candidateService.AddCandidateAsync(dto);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Candidate email not provided", result.Error);
        }

        [Fact]
        public async Task AddCandidateAsync_WithEmptyContactNumber_ReturnsFailureMessage()
        {
            // Arrange
            var dto = new CreateCandidateDTO
            {
                Name = "Milan M",
                Email = "milan.milan@test.com",
                ContactNumber = string.Empty
            };

            // Act
            var result = await _candidateService.AddCandidateAsync(dto);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Candidate contact number not provided", result.Error);
        }

        [Fact]
        public async Task AddCandidateAsync_WithInvalidContactNumberFormat_ReturnsFailureMessage()
        {
            // Arrange
            var dto = new CreateCandidateDTO
            {
                Name = "Milan M",
                Email = "milan.milan@test.com",
                ContactNumber = "123ABCXYZ"
            };

            // Act
            var result = await _candidateService.AddCandidateAsync(dto);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Candidate contact number format is invalid", result.Error);
        }

        [Fact]
        public async Task AddCandidateAsync_WithValidData_ReturnsSuccess()
        {
            // Arrange
            var dto = new CreateCandidateDTO
            {
                Name = "Milan M",
                Email = "milan.milan@test.com",
                ContactNumber = "+381601231231"
            };

            _repoMock.Setup(repo => repo.AddAsync(It.IsAny<Candidate>()))
                .ReturnsAsync((Candidate target) => target); 

            // Act
            var result = await _candidateService.AddCandidateAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(dto.Name, result.Value.Name);
        }

        [Fact]
        public async Task RemoveSkillFromCandidateAsync_WithEmptySkillName_ReturnsFailure()
        {
            // Arrange
            var candidateId = Guid.NewGuid();

            // Act
            var result = await _candidateService.RemoveSkillFromCandidateAsync(candidateId, string.Empty);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Skill name cannot be empty.", result.Error);
        }

        [Fact]
        public async Task RemoveSkillFromCandidateAsync_CandidateDoesNotPossessSkill_ReturnsFailure()
        {
            // Arrange
            var candidateId = Guid.NewGuid();
            var skillName = "C#";
            var candidate = new Candidate 
            { 
                Id = candidateId, 
                Name = "Milan", 
                CandidateSkills = [] 
            };

            _repoMock.Setup(repo => repo.GetByIdAsync(candidateId)).ReturnsAsync(candidate);

            // Act
            var result = await _candidateService.RemoveSkillFromCandidateAsync(candidateId, skillName);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal($"Candidate does not possess the skill '{skillName}'.", result.Error);
        }

        [Fact]
        public async Task RemoveSkillFromCandidateAsync_SkillRemovedSuccessfully_ReturnsSuccess()
        {
            // Arrange
            var candidateId = Guid.NewGuid();
            var skillName = "C#";
            var candidate = new Candidate 
            { 
                Id = candidateId, 
                Name = "Milan", 
                CandidateSkills = [
                    new CandidateSkills 
                    { 
                        CandidateId = candidateId, 
                        Skill = new Skill 
                        { 
                            Name = "C#"
                        } 
                    }
                ] 
            };

            _repoMock.Setup(repo => repo.GetByIdAsync(candidateId))
                     .ReturnsAsync(candidate);

            // Act
            var result = await _candidateService.RemoveSkillFromCandidateAsync(candidateId, skillName);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(candidate.CandidateSkills);
            _repoMock.Verify(repo => repo.SaveChangesAsync(), Times.Once); // Verifies that the changes were saved
        }
    }
}
