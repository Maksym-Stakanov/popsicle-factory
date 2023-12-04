using CSharpFunctionalExtensions;
using PopsicleFactory.Core.DTOs.Requests;
using PopsicleFactory.Core.DTOs.Responses;
using PopsicleFactory.Core.Entities;
using PopsicleFactory.Core.Extensions;
using PopsicleFactory.Core.Interfaces;
using PopsicleFactory.Core.Interfaces.Repositories;
using PopsicleFactory.Core.ValueObjects;

namespace PopsicleFactory.Core.UseCases;

public sealed class UpdatePopsicleNameUseCase : UseCase<UpdatePopsicleNameRequestDto, PopsicleResponseDto>
{
    private readonly PopsicleRepository popsicleRepository;
    private readonly UnitOfWork unitOfWork;

    public UpdatePopsicleNameUseCase(PopsicleRepository popsicleRepository, UnitOfWork unitOfWork)
    {
        this.popsicleRepository = popsicleRepository;
        this.unitOfWork = unitOfWork;
    }

    public Task Handle(UpdatePopsicleNameRequestDto request, Presenter<PopsicleResponseDto> presenter)
    {
        Popsicle popsicle = null!;
        
        return popsicleRepository.EntityBy(request.Id, unitOfWork)
            .Tap(popsicleEntity => popsicle = popsicleEntity)
            .Bind(_ => PopsicleNameObject.Create(request.Name))
            .Bind(name => popsicle.UpdateName(name))
            .Bind(unitOfWork.CommitAndDispatchAsync)
            .Map(_ => new PopsicleResponseDto(popsicle.Id, popsicle.Name.Value, popsicle.Size))
            .FinallyWithRollback(presenter, unitOfWork);
    }
}