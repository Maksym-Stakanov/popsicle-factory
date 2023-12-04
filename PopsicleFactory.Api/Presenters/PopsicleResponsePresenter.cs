using PopsicleFactory.Api.Models.Responses;
using PopsicleFactory.Core.DTOs.Responses;

namespace PopsicleFactory.Api.Presenters;

internal sealed class PopsicleResponsePresenter : BasePresenter<PopsicleResponseDto, PopsicleResponse>
{
    private readonly string apiBaseUrl;
    public PopsicleResponsePresenter(string apiBaseUrl) => this.apiBaseUrl = apiBaseUrl;
    protected override Task Handle(PopsicleResponseDto response)
        => Ok(
            new PopsicleResponse(
                response.Name,
                response.Size,
                $"{apiBaseUrl}/popsicles/{response.Id}"
            )
        );
}