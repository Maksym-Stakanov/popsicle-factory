using CSharpFunctionalExtensions;
using PopsicleFactory.Core.DTOs.Requests;
using PopsicleFactory.Core.DTOs.Responses;
using PopsicleFactory.Core.Extensions;
using PopsicleFactory.Core.Interfaces;
using PopsicleFactory.Core.Interfaces.Repositories;

namespace PopsicleFactory.Core.UseCases;

public sealed class GetPopsicleUseCase : UseCase<PopsicleRequestDto, PopsicleResponseDto> {
    private readonly PopsicleRepository popsicleRepository;

    public GetPopsicleUseCase(PopsicleRepository popsicleRepository, UnitOfWork _) {
        this.popsicleRepository = popsicleRepository;
    }

    public Task Handle(PopsicleRequestDto request, Presenter<PopsicleResponseDto> presenter) {
        return popsicleRepository.ObjectBy(request.Id)
            .Map(popsicle => new PopsicleResponseDto(popsicle.Id, popsicle.Name.Value, popsicle.Size))
            .Finally(presenter);
    }
}