using Microsoft.AspNetCore.Mvc;
using PopsicleFactory.Api.Models.Requests;
using PopsicleFactory.Api.Models.Responses;
using PopsicleFactory.Api.Presenters;
using PopsicleFactory.Core.DTOs.Requests;
using PopsicleFactory.Core.Enums;
using PopsicleFactory.Core.Interfaces;
using PopsicleFactory.Core.Interfaces.Repositories;
using PopsicleFactory.Core.UseCases;

namespace PopsicleFactory.Api.Controllers;

[ApiController]
[Route("api/popsicles/")]
public class PopsiclesController : BaseController
{
    private readonly PopsicleRepository popsicleRepository;

    public PopsiclesController(PopsicleRepository popsicleRepository, UnitOfWorkFactory unitOfWorkFactory)
        : base(unitOfWorkFactory)
    {
        this.popsicleRepository = popsicleRepository;
    }

    [HttpPost]
    public async Task<ActionResult<PopsicleResponse>> AddPopsicle([FromBody] AddPopsicleRequest request)
    {
        var requestDto = new AddPopsicleRequestDto(request.Name, request.Size);
        using var unitOfWork = unitOfWorkFactory.GetUnitOfWork();

        var useCase = new AddPopsicleUseCase(popsicleRepository, unitOfWork);
        var presenter = new PopsicleResponsePresenter(BaseUrl);

        return await ExecuteUseCase(useCase, requestDto, presenter);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<OkResponse>> DeletePopsicle(Guid id)
    {
        var requestDto = new DeletePopsicleRequestDto(id);
        using var unitOfWork = unitOfWorkFactory.GetUnitOfWork();

        var useCase = new DeletePopsicleUseCase(popsicleRepository, unitOfWork);
        var presenter = new OkResponsePresenter();

        return await ExecuteUseCase(useCase, requestDto, presenter);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PopsicleResponse>> Popsicle(Guid id)
    {
        var requestDto = new PopsicleRequestDto(id);
        using var unitOfWork = unitOfWorkFactory.GetUnitOfWork();

        var useCase = new GetPopsicleUseCase(popsicleRepository, unitOfWork);
        var presenter = new PopsicleResponsePresenter(BaseUrl);

        return await ExecuteUseCase(useCase, requestDto, presenter);
    }
    
    [HttpGet]
    public async Task<ActionResult<PopsiclesListResponse>> GetList(PopsicleSize? size = null)
    {
        var requestDto = new PopsiclesListRequestDto(size);
        using var unitOfWork = unitOfWorkFactory.GetUnitOfWork();

        var useCase = new GetPopsiclesListUseCase(popsicleRepository, unitOfWork);
        var presenter = new PopsiclesListResponsePresenter(BaseUrl);

        return await ExecuteUseCase(useCase, requestDto, presenter);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PopsicleResponse>> Update(Guid id, [FromBody] UpdatePopsicleRequest request)
    {
        var requestDto = new UpdatePopsicleRequestDto(id, request.Name, request.Size);
        using var unitOfWork = unitOfWorkFactory.GetUnitOfWork();

        var useCase = new UpdatePopsicleUseCase(popsicleRepository, unitOfWork);
        var presenter = new PopsicleResponsePresenter(BaseUrl);

        return await ExecuteUseCase(useCase, requestDto, presenter);
    }
    
    [HttpPatch("{id:guid}/update-name")]
    public async Task<ActionResult<PopsicleResponse>> UpdateName(Guid id, [FromBody] string name)
    {
        var requestDto = new UpdatePopsicleNameRequestDto(id, name);
        using var unitOfWork = unitOfWorkFactory.GetUnitOfWork();

        var useCase = new UpdatePopsicleNameUseCase(popsicleRepository, unitOfWork);
        var presenter = new PopsicleResponsePresenter(BaseUrl);

        return await ExecuteUseCase(useCase, requestDto, presenter);
    }
    
    [HttpPatch("{id:guid}/update-size")]
    public async Task<ActionResult<PopsicleResponse>> UpdateSize(Guid id, [FromBody] string size)
    {
        var requestDto = new UpdatePopsicleSizeRequestDto(id, size);
        using var unitOfWork = unitOfWorkFactory.GetUnitOfWork();

        var useCase = new UpdatePopsicleSizeUseCase(popsicleRepository, unitOfWork);
        var presenter = new PopsicleResponsePresenter(BaseUrl);

        return await ExecuteUseCase(useCase, requestDto, presenter);
    }
}