using PopsicleFactory.Core.DTOs.Requests.Base;

namespace PopsicleFactory.Core.DTOs.Requests;

public sealed record DeletePopsicleRequestDto(Guid Id) : RequestDto;