using Microsoft.AspNetCore.Mvc;
using PopsicleFactory.Api.Presenters;
using PopsicleFactory.Core.DTOs.Requests.Base;
using PopsicleFactory.Core.DTOs.Responses.Base;
using PopsicleFactory.Core.Interfaces;

namespace PopsicleFactory.Api.Controllers;

public class BaseController : ControllerBase
{
    protected readonly UnitOfWorkFactory unitOfWorkFactory;
    protected string BaseUrl => $"{Request.Scheme}://{Request.Host.Value}/api";
    
    public BaseController(UnitOfWorkFactory unitOfWorkFactory)
    {
        this.unitOfWorkFactory = unitOfWorkFactory;
    }
    
    internal async Task<ActionResult<TModel>> ExecuteUseCase<TModel, TRequest, TResponse>(
        UseCase<TRequest, TResponse> useCase,
        TRequest request,
        BasePresenter<TResponse, TModel> presenter
    ) where TRequest : RequestDto where TResponse : ResponseDto
    {
        await useCase.Handle(request, presenter);
        return presenter.ApiResponse!;
    }
}