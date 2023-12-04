using PopsicleFactory.Core.DTOs.Requests.Base;

namespace PopsicleFactory.Core.DTOs.Requests;

public sealed record UpdatePopsicleNameRequestDto(Guid Id, string Name) : RequestDto;