using CSharpFunctionalExtensions;
using PopsicleFactory.Core.DTOs.Requests;
using PopsicleFactory.Core.DTOs.Responses;
using PopsicleFactory.Core.Entities;
using PopsicleFactory.Core.Extensions;
using PopsicleFactory.Core.Interfaces;
using PopsicleFactory.Core.Interfaces.Repositories;
using PopsicleFactory.Core.ValueObjects;

namespace PopsicleFactory.Core.UseCases;

public sealed class UpdatePopsicleUseCase : UseCase<UpdatePopsicleRequestDto, PopsicleResponseDto>
{
    private readonly PopsicleRepository popsicleRepository;
    private readonly UnitOfWork unitOfWork;

    public UpdatePopsicleUseCase(PopsicleRepository popsicleRepository, UnitOfWork unitOfWork)
    {
        this.popsicleRepository = popsicleRepository;
        this.unitOfWork = unitOfWork;
    }

    public Task Handle(UpdatePopsicleRequestDto request, Presenter<PopsicleResponseDto> presenter)
    {
        Popsicle popsicle = null!;
        
        return popsicleRepository.EntityBy(request.Id, unitOfWork)
            .Tap(popsicleEntity => popsicle = popsicleEntity)
            .Bind(_ => PopsicleObject.Create(request.Id, request.Name, request.Size))
            .Bind(updatePopsicleObject => popsicle.Update(updatePopsicleObject))
            .Bind(unitOfWork.CommitAndDispatchAsync)
            .Map(_ => new PopsicleResponseDto(popsicle.Id, popsicle.Name.Value, popsicle.Size))
            .FinallyWithRollback(presenter, unitOfWork);
    }
}