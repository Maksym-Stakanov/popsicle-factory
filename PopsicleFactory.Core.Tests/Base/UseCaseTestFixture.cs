using PopsicleFactory.Core.Interfaces;
using PopsicleFactory.Core.Interfaces.Repositories;
using PopsicleFactory.InMemoryRepository;

namespace Popsicle.Factory.Core.Tests.Base;

public sealed class UseCaseTestFixture : IDisposable
{
    public UnitOfWork UnitOfWork { get; } = new InMemoryUnitOfWork();
    public PopsicleRepository PopsicleRepository { get; } = new PopsicleInMemoryRepository();

    public void Dispose()
    {
    }
}