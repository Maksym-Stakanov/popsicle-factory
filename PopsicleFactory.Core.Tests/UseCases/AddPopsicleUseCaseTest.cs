using Popsicle.Factory.Core.Tests.Base;
using PopsicleFactory.Core.DTOs.Requests;
using PopsicleFactory.Core.DTOs.Responses;
using PopsicleFactory.Core.Enums;
using PopsicleFactory.Core.Errors;
using PopsicleFactory.Core.UseCases;
using Xunit.Abstractions;

namespace Popsicle.Factory.Core.Tests.UseCases;

public sealed class AddPopsicleUseCaseTest : BaseUseCaseTest<AddPopsicleRequestDto, PopsicleResponseDto>
{
    public AddPopsicleUseCaseTest(UseCaseTestFixture fixture, ITestOutputHelper _) : base(fixture) =>
        UseCase = new AddPopsicleUseCase(Fixture.PopsicleRepository, Fixture.UnitOfWork);

    [Fact]
    public async Task When_RequestWithNotEmptyName_ShouldSucceed()
    {
        Request = new AddPopsicleRequestDto("Yummy popsicle", PopsicleSize.Big.ToString());
        await HandleRequestAsync();
        ExpectSuccess();
    }

    [Fact]
    public async Task When_RequestHasEmptyName_ShouldFail()
    {
        Request = new AddPopsicleRequestDto(string.Empty, PopsicleSize.Big.ToString());
        await HandleRequestAsync();
        ExpectError(PopsicleErrors.EmptyName);
    }

    [Fact]
    public async Task When_RequestWrongSize_ShouldFail()
    {
        Request = new AddPopsicleRequestDto("Strawberry", "SuperBig");
        await HandleRequestAsync();
        ExpectError(PopsicleErrors.IncorrectSize);
    }
}