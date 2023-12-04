using PopsicleFactory.Core.Interfaces;

namespace PopsicleFactory.InMemoryRepository;

public sealed class InMemoryUnitOfWorkFactory : UnitOfWorkFactory
{
    public UnitOfWork GetUnitOfWork() => new InMemoryUnitOfWork();
}