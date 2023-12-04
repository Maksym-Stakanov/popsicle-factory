using CSharpFunctionalExtensions;
using PopsicleFactory.Core.Interfaces;

namespace PopsicleFactory.InMemoryRepository;

public class InMemoryUnitOfWork : UnitOfWork
{
    public Task<Result<T>> CommitAndDispatchAsync<T>(T result)
    {
        return Task.FromResult(Result.Success(result));
    }
    public Task RollbackAndDiscardAsync()
    {
        return Task.CompletedTask;
    }
    public void Dispose()
    {
    }
}