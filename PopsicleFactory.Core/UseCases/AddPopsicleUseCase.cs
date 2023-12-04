using CSharpFunctionalExtensions;
using PopsicleFactory.Core.DTOs.Requests;
using PopsicleFactory.Core.DTOs.Responses;
using PopsicleFactory.Core.Entities;
using PopsicleFactory.Core.Extensions;
using PopsicleFactory.Core.Interfaces;
using PopsicleFactory.Core.Interfaces.Repositories;
using PopsicleFactory.Core.ValueObjects;

namespace PopsicleFactory.Core.UseCases;

public sealed class AddPopsicleUseCase : UseCase<AddPopsicleRequestDto, PopsicleResponseDto> {
    private readonly PopsicleRepository popsicleRepository;
    private readonly UnitOfWork unitOfWork;

    public AddPopsicleUseCase(PopsicleRepository popsicleRepository, UnitOfWork unitOfWork) {
        this.popsicleRepository = popsicleRepository;
        this.unitOfWork = unitOfWork;
    }

    public Task Handle(AddPopsicleRequestDto request, Presenter<PopsicleResponseDto> presenter)
    {
        var name = PopsicleNameObject.Create(request.Name);
        var size = PopsicleObject.ConvertPopsicleSize(request.Size);
        
        return Result.Combine(name, size)
            .Map(() => Popsicle.Create(name.Value, size.Value))
            .Bind(popsicle => popsicleRepository.Add(popsicle, unitOfWork))
            .Bind(unitOfWork.CommitAndDispatchAsync)
            .Map(popsicle => new PopsicleResponseDto(popsicle.Id, popsicle.Name.Value, popsicle.Size))
            .FinallyWithRollback(presenter, unitOfWork);
    }

}