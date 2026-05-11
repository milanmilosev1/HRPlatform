using HRPlatform.Domain.DTOs.SkillDTOs;
using HRPlatform.Domain.Models;
using HRPlatform.Domain.Repositories;
using HRPlatform.Services.SkillServices;
using Moq;

namespace HRPlatform.Tests.SkillServiceTests
{
    public class SkillServiceTests
    {
        private readonly Mock<ISkillRepository> _repoMock;
        private readonly SkillService _skillService;

        public SkillServiceTests()
        {
            _repoMock = new Mock<ISkillRepository>();
            _skillService = new SkillService(_repoMock.Object);
        }

        [Fact]
        public async Task AddSkillAsync_WithNullSkill_ReturnsFailure()
        {
            // Act
            var result = await _skillService.AddSkillAsync(null!);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Skill data cannot be null", result.Error);
        }

        [Fact]
        public async Task AddSkillAsync_WithValidData_ReturnsSuccess()
        {
            // Arrange
            var dto = new CreateSkillDTO
            {
                Name = "C#"
            };

            _repoMock.Setup(repo => repo.GetAllAsync())
                     .ReturnsAsync(new System.Collections.Generic.List<Skill>());
            _repoMock.Setup(repo => repo.AddAsync(It.IsAny<Skill>()))
                     .ReturnsAsync((Skill target) => target);

            // Act
            var result = await _skillService.AddSkillAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(dto.Name, result.Value.Name);
        }

        [Fact]
        public async Task AddSkillAsync_WithExistingName_ReturnsFailure()
        {
            // Arrange
            var dto = new CreateSkillDTO
            {
                Name = "Java"
            };

            var existingSkills = new System.Collections.Generic.List<Skill>
            {
                new Skill 
                { 
                    Id = Guid.NewGuid(),
                    Name = "Java",
                    CandidateSkills = []
                }
            };

            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(existingSkills);

            // Act
            var result = await _skillService.AddSkillAsync(dto);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal($"Skill with name '{dto.Name}' already exists.", result.Error);
        }

        [Fact]
        public async Task GetSkillByIdAsync_ExistingId_ReturnsSuccess()
        {
            // Arrange
            var skillId = Guid.NewGuid();
            var skill = new Skill 
            { 
                Id = skillId, 
                Name = "SQL", 
                CandidateSkills = []
            };

            _repoMock.Setup(repo => repo.GetByIdAsync(skillId)).ReturnsAsync(skill);

            // Act
            var result = await _skillService.GetSkillByIdAsync(skillId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(skillId, result.Value.Id);
        }

        [Fact]
        public async Task GetSkillByIdAsync_NonExistingId_ReturnsFailure()
        {
            // Arrange
            var skillId = Guid.NewGuid();
            _repoMock.Setup(repo => repo.GetByIdAsync(skillId)).ReturnsAsync((Skill)null!);

            // Act
            var result = await _skillService.GetSkillByIdAsync(skillId);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal($"Skill with id: {skillId} does not exist", result.Error);
        }

        [Fact]
        public async Task RemoveSkillAsync_NonExistingId_ReturnsFailure()
        {
            // Arrange
            var skillId = Guid.NewGuid();
            _repoMock.Setup(repo => repo.GetByIdAsync(skillId)).ReturnsAsync((Skill)null!);

            // Act
            var result = await _skillService.RemoveSkillAsync(skillId);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal($"Skill with id: {skillId} does not exist", result.Error);
        }
    }
}
