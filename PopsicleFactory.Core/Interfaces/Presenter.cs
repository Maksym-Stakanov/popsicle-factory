using CSharpFunctionalExtensions;

namespace PopsicleFactory.Core.Interfaces;

public interface Presenter<T>
{
    Task Handle(Result<T> response);
}