namespace PopsicleFactory.Core.Interfaces;

public interface UseCase<in TRequest, TResponse>
{
    Task Handle(TRequest request, Presenter<TResponse> presenter);
}
