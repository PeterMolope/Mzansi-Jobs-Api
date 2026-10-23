using FluentAssertions;
using Moq;
using MzansiJobs.Application.Common.Exceptions;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Candidates.Handlers;
using MzansiJobs.Application.Features.Candidates.Queries;
using MzansiJobs.Domain.Entities;


namespace MzansiJobs.Tests.Candidates.Queries
{
    public class GetCandidateProfileQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IGenericRepository<Candidate>> _mockCandidateRepo;
        private readonly GetCandidateProfileQueryHandler _handler;

        public GetCandidateProfileQueryHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCandidateRepo = new Mock<IGenericRepository<Candidate>>();

            _mockUnitOfWork
                .Setup(uow => uow.Repository<Candidate>())
                .Returns(_mockCandidateRepo.Object);

            _handler = new GetCandidateProfileQueryHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_CandidateExists_ShouldReturnCandidateProfileDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var candidate = new Candidate(userId, "Sipho", "Nkosi");

            var candidates = new List<Candidate> { candidate }.AsQueryable();
            _mockCandidateRepo.Setup(r => r.GetQueryable()).Returns(candidates);

            var query = new GetCandidateProfileQuery(candidate.ID);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(candidate.ID);
            result.FirstName.Should().Be("Sipho");
            result.LastName.Should().Be("Nkosi");
        }

        [Fact]
        public async Task Handle_CandidateNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            var candidates = new List<Candidate>().AsQueryable();
            _mockCandidateRepo.Setup(r => r.GetQueryable()).Returns(candidates);

            var query = new GetCandidateProfileQuery(nonExistentId);

            // Act
            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"*{nonExistentId}*");
        }
    }
}
