using PopsicleFactory.Core.DTOs.Requests.Base;

namespace PopsicleFactory.Core.DTOs.Requests;

public sealed record PopsicleRequestDto(Guid Id) : RequestDto;