using CSharpFunctionalExtensions;
using PopsicleFactory.Core.Entities;
using PopsicleFactory.Core.Enums;
using PopsicleFactory.Core.ValueObjects;

namespace PopsicleFactory.Core.Interfaces.Repositories;

public interface PopsicleRepository
{
    Task<Result<PopsicleObject>> ObjectBy(Guid id);
    Task<Result<List<PopsicleObject>>> ObjectsListBy(PopsicleSize? size = null);
    Task<Result<Popsicle>> EntityBy(Guid id, UnitOfWork unitOfWork); // TODO: add db repository
    Task<Result<Popsicle>> Delete(Guid id, UnitOfWork unitOfWork);
    Result<Popsicle> Add(Popsicle popsicle, UnitOfWork unitOfWork);
}