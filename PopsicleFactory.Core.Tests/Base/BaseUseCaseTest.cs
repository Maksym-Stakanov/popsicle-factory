using PopsicleFactory.Core.DTOs.Responses.Base;
using PopsicleFactory.Core.Interfaces;

namespace Popsicle.Factory.Core.Tests.Base;

public abstract class BaseUseCaseTest<TIn, TOut> : IClassFixture<UseCaseTestFixture> where TOut : ResponseDto
{
    protected UseCaseTestFixture Fixture { get; }
    protected UseCase<TIn, TOut>? UseCase;
    protected TIn? Request;
    protected readonly PresenterDouble<TOut> Presenter;

    protected BaseUseCaseTest(UseCaseTestFixture fixture)
    {
        Fixture = fixture;
        Presenter = new PresenterDouble<TOut>();
    }

    protected async Task HandleRequestAsync()
    {
        await UseCase!.Handle(Request!, Presenter);
    }

    protected void ExpectError(string error)
    {
        Assert.True(Presenter.Result.IsFailure, $"Expected {error}, but got success");
        Assert.Equal(error, Presenter.Result.Error);
    }

    protected void ExpectSuccess()
    {
        Assert.True(Presenter.Result.IsSuccess);
    }
}