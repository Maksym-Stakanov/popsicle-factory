using CSharpFunctionalExtensions;
using PopsicleFactory.Core.DTOs.Requests;
using PopsicleFactory.Core.DTOs.Responses;
using PopsicleFactory.Core.Extensions;
using PopsicleFactory.Core.Interfaces;
using PopsicleFactory.Core.Interfaces.Repositories;

namespace PopsicleFactory.Core.UseCases;

public sealed class GetPopsiclesListUseCase : UseCase<PopsiclesListRequestDto, PopsiclesListResponseDto>
{
    private readonly PopsicleRepository popsicleRepository;

    public GetPopsiclesListUseCase(PopsicleRepository popsicleRepository, UnitOfWork _)
    {
        this.popsicleRepository = popsicleRepository;
    }

    public Task Handle(PopsiclesListRequestDto request, Presenter<PopsiclesListResponseDto> presenter)
    {
        return popsicleRepository.ObjectsListBy(request.Size)
            .Map(popsiclesList =>
                new PopsiclesListResponseDto(
                    popsiclesList.Select(p => new PopsicleResponseDto(p.Id, p.Name.Value, p.Size))
                        .ToList()
                )
            )
            .Finally(presenter);
    }
}