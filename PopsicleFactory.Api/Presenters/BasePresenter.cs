using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PopsicleFactory.Core.DTOs.Responses.Base;
using PopsicleFactory.Core.Errors;
using PopsicleFactory.Core.Interfaces;

namespace PopsicleFactory.Api.Presenters;

public abstract class BasePresenter<TDto, TModel> : Presenter<TDto> where TDto : ResponseDto
{
    public ActionResult<TModel>? ApiResponse { get; private set; }

    public Task Handle(Result<TDto> response)
    {
        if (response.IsFailure)
        {
            if (string.Equals(response.Error, PopsicleErrors.NotFound))
            {
                ApiResponse = new NotFoundResult();
            } else if (string.Equals(response.Error, PopsicleErrors.FailedToAddNewPopsicle)
                       || string.Equals(response.Error, PopsicleErrors.FailedToRemovePopsicle))
            {
                ApiResponse = new ObjectResult(response.Error) { StatusCode = StatusCodes.Status500InternalServerError };
            } else
            {
                ApiResponse = new BadRequestObjectResult(response.Error);
            }

            return Task.CompletedTask;
        }

        return Handle(response.Value);
    }

    protected abstract Task Handle(TDto response);

    protected Task Ok(TModel model)
    {
        ApiResponse = new OkObjectResult(model);
        return Task.CompletedTask;
    }
    protected Task Deleted()
    {
        ApiResponse = new OkObjectResult(null) { StatusCode = StatusCodes.Status204NoContent};
        return Task.CompletedTask;
    }
}