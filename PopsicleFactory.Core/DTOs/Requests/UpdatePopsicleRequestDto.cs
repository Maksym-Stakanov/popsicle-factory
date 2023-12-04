using PopsicleFactory.Core.DTOs.Requests.Base;

namespace PopsicleFactory.Core.DTOs.Requests;

public sealed record UpdatePopsicleRequestDto(Guid Id, string Name, string Size) : RequestDto;