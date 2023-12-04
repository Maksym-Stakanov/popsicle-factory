using Popsicle.Factory.Core.Tests.Base;
using PopsicleFactory.Core.DTOs.Requests;
using PopsicleFactory.Core.DTOs.Responses;
using PopsicleFactory.Core.DTOs.Responses.Base;
using PopsicleFactory.Core.Enums;
using PopsicleFactory.Core.Errors;
using PopsicleFactory.Core.UseCases;
using Xunit.Abstractions;

namespace Popsicle.Factory.Core.Tests.UseCases;

public sealed class DeletePopsicleUseCaseTest : BaseUseCaseTest<DeletePopsicleRequestDto, OkResponseDto>
{
    private readonly AddPopsicleUseCase addPopsicleUseCase;
    private readonly PresenterDouble<PopsicleResponseDto> addPopsiclePresenter;

    public DeletePopsicleUseCaseTest(UseCaseTestFixture fixture, ITestOutputHelper _) : base(fixture)
    {
        UseCase = new DeletePopsicleUseCase(Fixture.PopsicleRepository, Fixture.UnitOfWork);

        addPopsicleUseCase = new AddPopsicleUseCase(Fixture.PopsicleRepository, Fixture.UnitOfWork);
        addPopsiclePresenter = new PresenterDouble<PopsicleResponseDto>();
    }

    [Fact]
    public async Task When_PopsicleDoesNotExist_ShouldFail()
    {
        Request = new DeletePopsicleRequestDto(Guid.NewGuid());
        await HandleRequestAsync();
        ExpectError(PopsicleErrors.NotFound);
    }

    [Fact]
    public async Task When_DeleteExistingPopsicle_ShouldSucceed()
    {
        await addPopsicleUseCase.Handle(new AddPopsicleRequestDto("Yummy", PopsicleSize.Big.ToString()), addPopsiclePresenter);

        Request = new DeletePopsicleRequestDto(addPopsiclePresenter.Result.Value.Id);
        await HandleRequestAsync();
        
        ExpectSuccess();
    }

    [Fact]
    public async Task When_DeleteExistingPopsicleTwice_ShouldSucceed()
    {
        await addPopsicleUseCase.Handle(new AddPopsicleRequestDto("Yummy", PopsicleSize.Big.ToString()), addPopsiclePresenter);

        Request = new DeletePopsicleRequestDto(addPopsiclePresenter.Result.Value.Id);
        await HandleRequestAsync();
        
        ExpectSuccess();
    }
}