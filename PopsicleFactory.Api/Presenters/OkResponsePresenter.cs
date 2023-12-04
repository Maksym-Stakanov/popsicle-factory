using PopsicleFactory.Api.Models.Responses;
using PopsicleFactory.Core.DTOs.Responses;
using PopsicleFactory.Core.DTOs.Responses.Base;

namespace PopsicleFactory.Api.Presenters;

internal sealed class OkResponsePresenter : BasePresenter<OkResponseDto, OkResponse>
{
    protected override Task Handle(OkResponseDto response) => Deleted();
}