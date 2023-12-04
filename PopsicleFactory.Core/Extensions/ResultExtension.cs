using CSharpFunctionalExtensions;
using PopsicleFactory.Core.Interfaces;

namespace PopsicleFactory.Core.Extensions;

public static class ResultExtension
{
    public static async Task FinallyWithRollback<T>(this Task<Result<T>> resultTask, Presenter<T> presenter, UnitOfWork unitOfWork) {
        var result = await resultTask.ConfigureAwait(false);
        if (result.IsFailure) await unitOfWork.RollbackAndDiscardAsync();

        unitOfWork.Dispose();

        await presenter.Handle(result).ConfigureAwait(false);
    }
    
    public static async Task Finally<T>(this Task<Result<T>> resultTask, Presenter<T> presenter) {
        var result = await resultTask.ConfigureAwait(false);
        await presenter.Handle(result).ConfigureAwait(false);
    }
}