using CSharpFunctionalExtensions;
using PopsicleFactory.Core.Interfaces;

namespace Popsicle.Factory.Core.Tests.Base
{
    public class PresenterDouble<TSuccess> : Presenter<TSuccess>
    {
        public Result<TSuccess> Result { get; private set; }
        public Task Handle(Result<TSuccess> response)
        {
            Result = response;

            return Task.CompletedTask;
        }
    }
}