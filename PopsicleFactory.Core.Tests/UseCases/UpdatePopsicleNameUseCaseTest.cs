using Popsicle.Factory.Core.Tests.Base;
using PopsicleFactory.Core.DTOs.Requests;
using PopsicleFactory.Core.DTOs.Responses;
using PopsicleFactory.Core.Enums;
using PopsicleFactory.Core.Errors;
using PopsicleFactory.Core.UseCases;
using Xunit.Abstractions;

namespace Popsicle.Factory.Core.Tests.UseCases;

public sealed class UpdatePopsicleNameUseCaseTest : BaseUseCaseTest<UpdatePopsicleNameRequestDto, PopsicleResponseDto>
{
    private readonly AddPopsicleUseCase addPopsicleUseCase;
    private readonly PresenterDouble<PopsicleResponseDto> addPopsiclePresenter;

    public UpdatePopsicleNameUseCaseTest(UseCaseTestFixture fixture, ITestOutputHelper _) : base(fixture)
    {
        UseCase = new UpdatePopsicleNameUseCase(Fixture.PopsicleRepository, Fixture.UnitOfWork);

        addPopsicleUseCase = new AddPopsicleUseCase(Fixture.PopsicleRepository, Fixture.UnitOfWork);
        addPopsiclePresenter = new PresenterDouble<PopsicleResponseDto>();
    }

    [Fact]
    public async Task When_PopsicleDoesNotExist_ShouldFail()
    {
        Request = new UpdatePopsicleNameRequestDto(Guid.NewGuid(), "New name");
        await HandleRequestAsync();
        ExpectError(PopsicleErrors.NotFound);
    }

    [Fact]
    public async Task When_UpdateExistingPopsicleName_ShouldSucceed()
    {
        await addPopsicleUseCase.Handle(new AddPopsicleRequestDto("Yummy", PopsicleSize.Big.ToString()), addPopsiclePresenter);

        Request = new UpdatePopsicleNameRequestDto(addPopsiclePresenter.Result.Value.Id, "New name");
        await HandleRequestAsync();
        
        ExpectSuccess();
        Assert.Equal(addPopsiclePresenter.Result.Value.Id, Presenter.Result.Value.Id);
        Assert.Equal(Request.Name, Presenter.Result.Value.Name);
        Assert.Equal(addPopsiclePresenter.Result.Value.Size, Presenter.Result.Value.Size);
    }
}