using Popsicle.Factory.Core.Tests.Base;
using PopsicleFactory.Core.DTOs.Requests;
using PopsicleFactory.Core.DTOs.Responses;
using PopsicleFactory.Core.Enums;
using PopsicleFactory.Core.Errors;
using PopsicleFactory.Core.UseCases;
using Xunit.Abstractions;

namespace Popsicle.Factory.Core.Tests.UseCases;

public sealed class GetPopsicleUseCaseTest : BaseUseCaseTest<PopsicleRequestDto, PopsicleResponseDto>
{
    private readonly AddPopsicleUseCase addPopsicleUseCase;
    private readonly PresenterDouble<PopsicleResponseDto> addPopsiclePresenter;

    public GetPopsicleUseCaseTest(UseCaseTestFixture fixture, ITestOutputHelper _) : base(fixture)
    {
        UseCase = new GetPopsicleUseCase(Fixture.PopsicleRepository, Fixture.UnitOfWork);

        addPopsicleUseCase = new AddPopsicleUseCase(Fixture.PopsicleRepository, Fixture.UnitOfWork);
        addPopsiclePresenter = new PresenterDouble<PopsicleResponseDto>();
    }

    [Fact]
    public async Task When_PopsicleDoesNotExist_ShouldFail()
    {
        Request = new PopsicleRequestDto(Guid.NewGuid());
        await HandleRequestAsync();
        ExpectError(PopsicleErrors.NotFound);
    }

    [Fact]
    public async Task When_PopsicleExists_ShouldSucceed()
    {
        await addPopsicleUseCase.Handle(new AddPopsicleRequestDto("Yummy", PopsicleSize.Big.ToString()), addPopsiclePresenter);

        Request = new PopsicleRequestDto(addPopsiclePresenter.Result.Value.Id);
        await HandleRequestAsync();
        ExpectSuccess();
        Assert.Equal(addPopsiclePresenter.Result.Value.Id, Presenter.Result.Value.Id);
    }
}