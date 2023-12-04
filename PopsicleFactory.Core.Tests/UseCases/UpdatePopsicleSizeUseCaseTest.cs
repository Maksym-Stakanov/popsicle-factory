using Popsicle.Factory.Core.Tests.Base;
using PopsicleFactory.Core.DTOs.Requests;
using PopsicleFactory.Core.DTOs.Responses;
using PopsicleFactory.Core.Enums;
using PopsicleFactory.Core.Errors;
using PopsicleFactory.Core.UseCases;
using Xunit.Abstractions;

namespace Popsicle.Factory.Core.Tests.UseCases;

public sealed class UpdatePopsicleSizeUseCaseTest : BaseUseCaseTest<UpdatePopsicleSizeRequestDto, PopsicleResponseDto>
{
    private readonly AddPopsicleUseCase addPopsicleUseCase;
    private readonly PresenterDouble<PopsicleResponseDto> addPopsiclePresenter;

    public UpdatePopsicleSizeUseCaseTest(UseCaseTestFixture fixture, ITestOutputHelper _) : base(fixture)
    {
        UseCase = new UpdatePopsicleSizeUseCase(Fixture.PopsicleRepository, Fixture.UnitOfWork);

        addPopsicleUseCase = new AddPopsicleUseCase(Fixture.PopsicleRepository, Fixture.UnitOfWork);
        addPopsiclePresenter = new PresenterDouble<PopsicleResponseDto>();
    }

    [Fact]
    public async Task When_PopsicleDoesNotExist_ShouldFail()
    {
        Request = new UpdatePopsicleSizeRequestDto(Guid.NewGuid(), "Small");
        await HandleRequestAsync();
        ExpectError(PopsicleErrors.NotFound);
    }

    [Fact]
    public async Task When_UpdateExistingPopsicleSize_ShouldSucceed()
    {
        await addPopsicleUseCase.Handle(new AddPopsicleRequestDto("Yummy", PopsicleSize.Big.ToString()), addPopsiclePresenter);

        Request = new UpdatePopsicleSizeRequestDto(addPopsiclePresenter.Result.Value.Id, PopsicleSize.Small.ToString());
        await HandleRequestAsync();
        
        ExpectSuccess();
        Assert.Equal(addPopsiclePresenter.Result.Value.Id, Presenter.Result.Value.Id);
        Assert.Equal(Request.Size, Presenter.Result.Value.Size.ToString());
        Assert.NotEqual(addPopsiclePresenter.Result.Value.Size, Presenter.Result.Value.Size);
    }
}