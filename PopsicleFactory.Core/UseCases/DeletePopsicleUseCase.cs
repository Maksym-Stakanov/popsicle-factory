using CSharpFunctionalExtensions;
using PopsicleFactory.Core.DTOs.Requests;
using PopsicleFactory.Core.DTOs.Responses.Base;
using PopsicleFactory.Core.Extensions;
using PopsicleFactory.Core.Interfaces;
using PopsicleFactory.Core.Interfaces.Repositories;

namespace PopsicleFactory.Core.UseCases;

public sealed class DeletePopsicleUseCase : UseCase<DeletePopsicleRequestDto, OkResponseDto> {
    private readonly PopsicleRepository popsicleRepository;
    private readonly UnitOfWork unitOfWork;

    public DeletePopsicleUseCase(PopsicleRepository popsicleRepository, UnitOfWork unitOfWork) {
        this.popsicleRepository = popsicleRepository;
        this.unitOfWork = unitOfWork;
    }

    public Task Handle(DeletePopsicleRequestDto request, Presenter<OkResponseDto> presenter) =>
        popsicleRepository.Delete(request.Id, unitOfWork)
            .Bind(unitOfWork.CommitAndDispatchAsync)
            .Map(_ => new OkResponseDto())
            .FinallyWithRollback(presenter, unitOfWork);
}