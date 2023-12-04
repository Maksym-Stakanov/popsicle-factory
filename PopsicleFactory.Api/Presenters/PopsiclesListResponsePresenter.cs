using PopsicleFactory.Api.Models.Responses;
using PopsicleFactory.Core.DTOs.Responses;

namespace PopsicleFactory.Api.Presenters;

internal sealed class PopsiclesListResponsePresenter : BasePresenter<PopsiclesListResponseDto, PopsiclesListResponse>
{
    private readonly string apiBaseUrl;
    public PopsiclesListResponsePresenter(string apiBaseUrl) => this.apiBaseUrl = apiBaseUrl;
    protected override Task Handle(PopsiclesListResponseDto response)
    {
        return Ok(
            new PopsiclesListResponse(
                response.Popsicles.Count,
                response.Popsicles.Select(popsicleDto =>
                    new PopsicleResponse(
                        popsicleDto.Name,
                        popsicleDto.Size,
                        $"{apiBaseUrl}/popsicles/{popsicleDto.Id}"
                    )
                ).ToArray()
            )
        );
    }
}