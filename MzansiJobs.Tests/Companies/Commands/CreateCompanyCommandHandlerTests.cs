using FluentAssertions;
using Moq;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Companies.Commands;
using MzansiJobs.Application.Features.Companies.Handlers;
using MzansiJobs.Domain.Entities;
using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Tests.Companies.Commands
{
    public class CreateCompanyCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IGenericRepository<Company>> _mockCompanyRepo;
        private readonly CreateCompanyCommandHandler _handler;

        public CreateCompanyCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCompanyRepo = new Mock<IGenericRepository<Company>>();

            _mockUnitOfWork
                .Setup(uow => uow.Repository<Company>())
                .Returns(_mockCompanyRepo.Object);

            _handler = new CreateCompanyCommandHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldAddCompanyAndReturnNewId()
        {
            // Arrange
            var command = new CreateCompanyCommand(
                Name: "Raven Wing",
                Description: "Custom software engineering and agency services.",
                Province: Province.Gauteng,
                Industry: Industry.InformationTechnology
            );

            // Act
            var resultId = await _handler.Handle(command, CancellationToken.None);

            // Assert

            resultId.Should().NotBeEmpty();

            _mockCompanyRepo.Verify(r => r.AddAsync(It.Is<Company>(c =>
                c.Name == "Raven Wing" &&
                c.Province == Province.Gauteng &&
                c.Industry == Industry.InformationTechnology
            )), Times.Once);

            _mockUnitOfWork.Verify(unitOW => unitOW.CompleteAsync(), Times.Once);
        }
    }
}
