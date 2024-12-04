using AutoMapper;
using Fora.Challenge.Application.Contracts.Persistance;
using Fora.Challenge.Application.Features.Categories.Commands;
using Fora.Challenge.Application.Features.Profiles;
using Fora.Challenge.Application.UnitTests.Mocks;
using Fora.Challenge.Domain.Entities;
using Moq;
using Shouldly;

namespace Fora.Challenge.Application.UnitTests.Categories.Commands
{
    public class CreateCategoryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Category>> _mockCategoryRepository;

        public CreateCategoryTests()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCategory_AddedToCategoriesRepo()
        {
            // Arrange
            var handler = new CreateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);

            // Act
            await handler.Handle(new CreateCategoryCommand() { Name = "Test" }, CancellationToken.None);

            // Assert
            var allCategories = await _mockCategoryRepository.Object.GetAllAsync();
            allCategories.Count.ShouldBe(5);
        }
    }
}
