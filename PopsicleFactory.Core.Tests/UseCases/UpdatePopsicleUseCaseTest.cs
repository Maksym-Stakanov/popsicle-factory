using Popsicle.Factory.Core.Tests.Base;
using PopsicleFactory.Core.DTOs.Requests;
using PopsicleFactory.Core.DTOs.Responses;
using PopsicleFactory.Core.Enums;
using PopsicleFactory.Core.Errors;
using PopsicleFactory.Core.UseCases;
using Xunit.Abstractions;

namespace Popsicle.Factory.Core.Tests.UseCases;

public sealed class UpdatePopsicleUseCaseTest : BaseUseCaseTest<UpdatePopsicleRequestDto, PopsicleResponseDto>
{
    private readonly AddPopsicleUseCase addPopsicleUseCase;
    private readonly PresenterDouble<PopsicleResponseDto> addPopsiclePresenter;

    public UpdatePopsicleUseCaseTest(UseCaseTestFixture fixture, ITestOutputHelper _) : base(fixture)
    {
        UseCase = new UpdatePopsicleUseCase(Fixture.PopsicleRepository, Fixture.UnitOfWork);

        addPopsicleUseCase = new AddPopsicleUseCase(Fixture.PopsicleRepository, Fixture.UnitOfWork);
        addPopsiclePresenter = new PresenterDouble<PopsicleResponseDto>();
    }

    [Fact]
    public async Task When_PopsicleDoesNotExist_ShouldFail()
    {
        Request = new UpdatePopsicleRequestDto(Guid.NewGuid(), "New name", PopsicleSize.Big.ToString());
        await HandleRequestAsync();
        ExpectError(PopsicleErrors.NotFound);
    }

    [Fact]
    public async Task When_UpdateExistingPopsicle_ShouldSucceed()
    {
        await addPopsicleUseCase.Handle(new AddPopsicleRequestDto("Yummy", PopsicleSize.Big.ToString()), addPopsiclePresenter);

        Request = new UpdatePopsicleRequestDto(addPopsiclePresenter.Result.Value.Id, "New name", PopsicleSize.Small.ToString());
        await HandleRequestAsync();
        
        ExpectSuccess();
        Assert.Equal(addPopsiclePresenter.Result.Value.Id, Presenter.Result.Value.Id);
        Assert.Equal(Request.Name, Presenter.Result.Value.Name);
        Assert.Equal(Request.Size, Presenter.Result.Value.Size.ToString());
    }
}