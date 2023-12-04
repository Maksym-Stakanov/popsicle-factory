using Popsicle.Factory.Core.Tests.Base;
using PopsicleFactory.Core.DTOs.Requests;
using PopsicleFactory.Core.DTOs.Responses;
using PopsicleFactory.Core.Enums;
using PopsicleFactory.Core.UseCases;
using PopsicleFactory.InMemoryRepository;
using Xunit.Abstractions;

namespace Popsicle.Factory.Core.Tests.UseCases;

public sealed class GetPopsiclesListUseCaseTest : BaseUseCaseTest<PopsiclesListRequestDto, PopsiclesListResponseDto>
{
    private readonly AddPopsicleUseCase addPopsicleUseCase;
    private readonly PresenterDouble<PopsicleResponseDto> addPopsiclePresenter;

    public GetPopsiclesListUseCaseTest(UseCaseTestFixture fixture, ITestOutputHelper _) : base(fixture)
    {
        var popsicleRepository = new PopsicleInMemoryRepository(); // Create new on every .ctor call
        
        UseCase = new GetPopsiclesListUseCase(popsicleRepository, fixture.UnitOfWork);

        addPopsicleUseCase = new AddPopsicleUseCase(popsicleRepository, fixture.UnitOfWork);
        addPopsiclePresenter = new PresenterDouble<PopsicleResponseDto>();
    }

    [Fact]
    public async Task When_PopsiclesDoNotExist_ShouldSucceed()
    {
        Request = new PopsiclesListRequestDto(null);
        await HandleRequestAsync();
        ExpectSuccess();
    }

    [Fact]
    public async Task When_GetAllExistingPopsicle_ShouldGetFour()
    {
        await addPopsicleUseCase.Handle(new AddPopsicleRequestDto("Yummy", PopsicleSize.Big.ToString()), addPopsiclePresenter);
        await addPopsicleUseCase.Handle(new AddPopsicleRequestDto("Strawberry", PopsicleSize.Medium.ToString()), addPopsiclePresenter);
        await addPopsicleUseCase.Handle(new AddPopsicleRequestDto("Apple", PopsicleSize.Big.ToString()), addPopsiclePresenter);
        await addPopsicleUseCase.Handle(new AddPopsicleRequestDto("Mix", PopsicleSize.Big.ToString()), addPopsiclePresenter);

        Request = new PopsiclesListRequestDto(null);
        await HandleRequestAsync();
        ExpectSuccess();
        Assert.Equal(4, Presenter.Result.Value.Popsicles.Count);
    }

    [Fact]
    public async Task When_GetOnlyBigExistingPopsicle_ShouldGetThree()
    {
        await addPopsicleUseCase.Handle(new AddPopsicleRequestDto("Yummy", PopsicleSize.Big.ToString()), addPopsiclePresenter);
        await addPopsicleUseCase.Handle(new AddPopsicleRequestDto("Strawberry", PopsicleSize.Medium.ToString()), addPopsiclePresenter);
        await addPopsicleUseCase.Handle(new AddPopsicleRequestDto("Apple", PopsicleSize.Big.ToString()), addPopsiclePresenter);
        await addPopsicleUseCase.Handle(new AddPopsicleRequestDto("Mix", PopsicleSize.Big.ToString()), addPopsiclePresenter);

        Request = new PopsiclesListRequestDto(PopsicleSize.Big);
        await HandleRequestAsync();
        ExpectSuccess();
        Assert.Equal(3, Presenter.Result.Value.Popsicles.Count);
    }
}