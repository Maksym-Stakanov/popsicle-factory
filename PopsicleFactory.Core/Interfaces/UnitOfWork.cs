using CSharpFunctionalExtensions;

namespace PopsicleFactory.Core.Interfaces;

public interface UnitOfWork : IDisposable
{
    Task<Result<T>> CommitAndDispatchAsync<T>(T result);
    Task RollbackAndDiscardAsync();
}