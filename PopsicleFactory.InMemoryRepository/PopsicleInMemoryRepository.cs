using System.Collections.Concurrent;
using CSharpFunctionalExtensions;
using PopsicleFactory.Core.Entities;
using PopsicleFactory.Core.Enums;
using PopsicleFactory.Core.Errors;
using PopsicleFactory.Core.Interfaces;
using PopsicleFactory.Core.Interfaces.Repositories;
using PopsicleFactory.Core.ValueObjects;

namespace PopsicleFactory.InMemoryRepository;

public sealed class PopsicleInMemoryRepository : PopsicleRepository
{
    private readonly ConcurrentDictionary<Guid, Popsicle> popsicles = new();
    private readonly object lockObject = new();

    public Task<Result<PopsicleObject>> ObjectBy(Guid id)
    {
        lock (lockObject)
        {
            return Task.FromResult(
                !popsicles.TryGetValue(id, out var popsicle)
                    ? Result.Failure<PopsicleObject>(PopsicleErrors.NotFound)
                    : Result.Success(popsicle.ToObject())
            );
        }
    }

    public Task<Result<List<PopsicleObject>>> ObjectsListBy(PopsicleSize? size = null)
    {
        lock (lockObject)
        {
            return Task.FromResult(Result.Success(
                popsicles.Values
                    .Where(p => size is null || p.Size == size.Value)
                    .OrderBy(p => p.Name.Value)
                    .ThenBy(p => p.Size.ToString())
                    .Select(p => p.ToObject())
                    .ToList()
            ));
        }
    }

    public Task<Result<Popsicle>> EntityBy(Guid id, UnitOfWork _)
    {
        lock (lockObject)
        {
            return Task.FromResult(
                !popsicles.TryGetValue(id, out var popsicle)
                    ? Result.Failure<Popsicle>(PopsicleErrors.NotFound)
                    : Result.Success(popsicle)
            );
        }
    }

    public Task<Result<Popsicle>> Delete(Guid id, UnitOfWork _)
    {
        lock (lockObject)
        {
            return Task.FromResult(
                !popsicles.TryGetValue(id, out var popsicle)
                    ? Result.Failure<Popsicle>(PopsicleErrors.NotFound)
                    : !popsicles.TryRemove(id, out popsicle)
                        ? Result.Failure<Popsicle>(PopsicleErrors.FailedToRemovePopsicle)
                        : Result.Success(popsicle)
            );
        }
    }

    public Result<Popsicle> Add(Popsicle popsicle, UnitOfWork _)
    {
        lock (lockObject)
        {
            if (popsicles.ContainsKey(popsicle.Id))
            {
                return Result.Failure<Popsicle>(PopsicleErrors.AlreadyExists);
            }

            return !popsicles.TryAdd(popsicle.Id, popsicle)
                ? Result.Failure<Popsicle>(PopsicleErrors.FailedToAddNewPopsicle)
                : Result.Success(popsicle);
        }
    }
}